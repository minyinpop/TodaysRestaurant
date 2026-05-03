using System;
using System.Collections;
using System.Collections.Generic;
using Audio_System.Data;
using Audio_System.Main;
using Restaurant_System.Object;
using Restaurant_System.Object.Creature.Customer.System.Main;
using UnityEngine;

namespace Restaurant_System.System.Child
{
    public sealed class CustomerManagerSystem : MonoBehaviour
    {
        [field: Header("顧客關鍵點")]
        [field: SerializeField] private Transform CustomerSpawnPoint;
        [field: SerializeField] private Transform CustomerParent;
        [field: SerializeField] private GameObject CustomerPrefab;
        [field: SerializeField] private int CustomerAmountPerRound;
        [field: SerializeField] private int CustomerComeDuration;
        
        [field: Header("椅子位置點")]
        [field: SerializeField] private SeatPoint[] SeatPoints;
        
        [field: Header("音效")]
        [field: SerializeField] private PlaySFXData customerSpawnSFX;
        
        private int _currentCustomerAmount;
        
        private IEnumerator _mainCoroutine;
        
        // private readonly Dictionary<Customer, SeatPoint> _customers = new();
        private readonly Queue<Action> _cleanUpActions = new();

        public event Action CustomerSpawned;
        public event Action CustomerHappy;
        public event Action CustomerAngry;
        
        private void OnDisable()
        {
            if (_mainCoroutine is not null)
            {
                StopCoroutine(_mainCoroutine);
                _mainCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            foreach (var cleanUp in _cleanUpActions)
            {
                cleanUp?.Invoke();
            }
        }

        #region System
            public void StartSystem()
            {
                SpawnCustomer();
            }

            public void EndSystem()
            {
                Debug.Log("已執行顧客系統關閉，但還沒有撰寫程式碼。");
            }
        #endregion

        #region Customer
            private void SpawnCustomer()
            {
                _mainCoroutine = SpawnCustomerProcess();
                StartCoroutine(_mainCoroutine);
            }

            private IEnumerator SpawnCustomerProcess()
            {
                while (_currentCustomerAmount < CustomerAmountPerRound)
                {
                    foreach (var seatPoint in SeatPoints)
                    {
                        #region 判斷是否可以生成新的顧客
                            if (seatPoint.IsOccupied())
                            {
                                continue;
                            }
                        #endregion

                        #region 顧客生成廣播
                            CustomerSpawned?.Invoke();
                        #endregion

                        #region 播放音效
                            AudioSystem.Instance.OtherSFX.PlayOneShot(customerSpawnSFX);
                        #endregion
                        
                        _currentCustomerAmount++;
                        
                        #region 生成顧客
                            var newCustomer = Instantiate(CustomerPrefab, CustomerSpawnPoint.position, Quaternion.identity, CustomerParent).GetComponent<Customer>();
                        #endregion

                        #region 設定顧客的行為
                            // _customers.Add(newCustomer, seatPoint);
                            
                            seatPoint.SetCustomer(newCustomer);
                            newCustomer.GiveServingNote(seatPoint.ServingNote());
                            newCustomer.WalkToSeatPoint(seatPoint.StandPoint(), seatPoint.SitPoint());
                            
                            newCustomer.HappyToLeave += HappyToLeave;
                            newCustomer.AngryToLeave += AngryToLeave;
                            
                            _cleanUpActions.Enqueue(() =>
                            {
                                newCustomer.HappyToLeave -= HappyToLeave;
                                newCustomer.AngryToLeave -= AngryToLeave;
                            });
                        #endregion
                        
                        yield return new WaitForSeconds(CustomerComeDuration);
                        
                        continue;
                        
                        void HappyToLeave()
                        {
                            if (CustomerHappy is null)
                            {
                                Debug.Log($"{nameof(CustomerHappy)} 沒有被訂閱。");
                                return;
                            }
                            
                            CustomerHappy.Invoke();

                            seatPoint.Reset();
                            newCustomer.WalkToEntrance(seatPoint.StandPoint(), CustomerSpawnPoint, () => Destroy(newCustomer.gameObject));
                        }
                        
                        void AngryToLeave()
                        {
                            if (CustomerAngry is null)
                            {
                                Debug.Log($"{nameof(CustomerAngry)} 沒有被訂閱。");
                                return;
                            }
                            
                            CustomerAngry.Invoke();

                            seatPoint.Reset();
                            newCustomer.WalkToEntrance(seatPoint.StandPoint(), CustomerSpawnPoint, () => Destroy(newCustomer.gameObject));
                        }
                    }

                    yield return null;
                }
            }
        #endregion
    }
}