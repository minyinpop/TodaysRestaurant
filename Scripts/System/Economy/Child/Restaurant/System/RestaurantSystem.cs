using System.Collections;
using System.Economy.Child.Customer.Main;
using Data.Economy.Restaurant_System;
using UnityEngine;

namespace System.Economy.Child.Restaurant.System
{
    internal sealed class RestaurantSystem : MonoBehaviour
    {
        [field: Header("Customer")]
        [field: SerializeField] private Transform CustomerSpawnPoint;
        [field: SerializeField] private Transform CustomerParent;
        [field: SerializeField] private GameObject CustomerPrefab;
        [field: SerializeField] private int CustomerAmountPerRound;
        [field: SerializeField] private int CustomerComeDuration;
        
        [field: Header("Points")]
        [field: SerializeField] private RestaurantPoint QueuePoints;
        [field: SerializeField] private RestaurantPoint SeatPoints;
        
        private int CurrentCustomerAmount;
        
        private IEnumerator QueueCor;

        private void Awake()
        {
            CustomerAmountPerRound = Mathf.Abs(CustomerAmountPerRound);
            CustomerComeDuration = Mathf.Abs(CustomerComeDuration);
        }

        private void OnDisable()
        {
            StopQueueCoroutine();
        }

        public void StartSystem()
        {
            QueueCor = QueueCoroutine();
            StartCoroutine(QueueCor);
            return;

            IEnumerator QueueCoroutine()
            {
                while (CurrentCustomerAmount < CustomerAmountPerRound)
                {
                    Debug.Log("判斷中 ......");
                    
                    SeatPoints.TryGetEmptyPoint(out var haveEmptySeatPoint, out var seatPoint);
                    
                    if (haveEmptySeatPoint)
                    {
                        QueuePoints.GetFirstPoint(out var queuePoint);
                        queuePoint.GetCustomer(out var haveCustomer, out var customer);
                        if (haveCustomer)
                        {
                            // ================================================
                            // 有顧客在排隊，直接請第一位排隊的顧客到空的位置，後面的顧客往前
                            // ================================================
                            seatPoint.SetCustomer(customer);
                            seatPoint.GetStandPoint(out var standPoint);
                            seatPoint.GetSitPoint(out var sitPoint);
                            
                            customer.WalkToSeatPoint(standPoint, sitPoint,
                                onArrive: () => Debug.Log($"{customer.name} 坐在 {seatPoint.name} 上了。"));
                            
                            // TODO 後面的顧客往前
                        }
                        else
                        {
                            // ================================================
                            // 沒有顧客在排隊，直接生成一位顧客，進到餐廳裡坐
                            // ================================================
                            CurrentCustomerAmount += 1;

                            var newCustomer = Instantiate(CustomerPrefab, CustomerSpawnPoint.position, Quaternion.identity, CustomerParent).GetComponent<CustomerSystem>();

                            seatPoint.SetCustomer(newCustomer);
                            seatPoint.GetStandPoint(out var standPoint);
                            seatPoint.GetSitPoint(out var sitPoint);

                            newCustomer.WalkToSeatPoint(standPoint, sitPoint,
                                onArrive: () => Debug.Log($"{newCustomer.name} 坐在 {seatPoint.name} 上了。"));
                        }
                    }
                    else
                    {
                        // ================================================
                        // 沒有空位，生成顧客去排隊，直到隊伍滿人
                        // ================================================
                        QueuePoints.TryGetEmptyPoint(out var haveEmptyQueuePoint, out var queuePoint);
                        if (!haveEmptyQueuePoint) { yield return null; continue; }
                        CurrentCustomerAmount += 1;
                    
                        var newCustomer = Instantiate(CustomerPrefab, CustomerSpawnPoint.position, Quaternion.identity, CustomerParent).GetComponent<CustomerSystem>();
                        queuePoint.SetCustomer(newCustomer);
                        queuePoint.GetStandPoint(out var standPoint);
                    
                        newCustomer.WalkToQueuePoint(standPoint);
                    }
                    
                    yield return new WaitForSeconds(CustomerComeDuration);
                }
            }
        }

        private void StopQueueCoroutine()
        {
            if (QueueCor is null) return;
            StopCoroutine(QueueCor);
            QueueCor = null;
        }
    }
}