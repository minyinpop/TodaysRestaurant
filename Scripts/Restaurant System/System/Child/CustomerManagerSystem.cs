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
        
        private readonly Dictionary<Customer, SeatPoint> _customers = new();
        private readonly Queue<Action> _cleanUpActions = new();
        
        private void OnDisable()
        {
            EndSystem();
        }

        #region System
            public void StartSystem()
            {
                SpawnCustomer();
            }

            public void EndSystem()
            {
                foreach (var cleanUp in _cleanUpActions)
                {
                    cleanUp?.Invoke();
                }

                if (_mainCoroutine is not null)
                {
                    StopCoroutine(_mainCoroutine);
                    _mainCoroutine = null;
                }
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
                        if (seatPoint.IsOccupied())
                        {
                            continue;
                        }
                        
                        AudioSystem.Instance.OtherSFX.PlayOneShot(customerSpawnSFX);
                        
                        _currentCustomerAmount++;
                        
                        var newCustomer = Instantiate(CustomerPrefab, CustomerSpawnPoint.position, Quaternion.identity, CustomerParent).GetComponent<Customer>();
                        
                        _customers.Add(newCustomer, seatPoint);
                        
                        seatPoint.SetCustomer(newCustomer);
                        newCustomer.GiveServingNote(seatPoint.ServingNote());
                        newCustomer.WalkToSeatPoint(seatPoint.StandPoint(), seatPoint.SitPoint());
                        
                        newCustomer.PrepareToLeave += Leave;
                        _cleanUpActions.Enqueue(() =>
                        {
                            newCustomer.PrepareToLeave -= Leave;
                        });
                        
                        yield return new WaitForSeconds(CustomerComeDuration);
                        
                        continue;
                        
                        void Leave()
                        {
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