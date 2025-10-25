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
        private IEnumerator SeatCor;

        private void Awake()
        {
            CustomerAmountPerRound = Mathf.Abs(CustomerAmountPerRound);
            CustomerComeDuration = Mathf.Abs(CustomerComeDuration);
        }

        private void OnDisable()
        {
            StopQueueCoroutine();
            StopSeatCoroutine();
        }

        public void StartSystem()
        {
            QueueCor = QueueCoroutine();
            StartCoroutine(QueueCor);
            
            SeatCor = SeatCoroutine();
            StartCoroutine(SeatCor);
            return;

            IEnumerator QueueCoroutine()
            {
                // TODO 先以 while 製作，之後再改成 Queue 有變更後呼叫
                while (CurrentCustomerAmount < CustomerAmountPerRound)
                {
                    QueuePoints.TryGetEmptyPoint(out var haveEmptyQueuePoint, out var queuePoint);
                    if (haveEmptyQueuePoint)
                    {
                        CurrentCustomerAmount += 1;

                        var customer = Instantiate(CustomerPrefab, CustomerSpawnPoint.position, Quaternion.identity, CustomerParent).GetComponent<CustomerSystem>();
                        queuePoint.SetCustomer(customer);
                        queuePoint.GetStandPoint(out var standPoint);

                        customer.WalkToQueuePoint(standPoint);
                        yield return new WaitForSeconds(CustomerComeDuration);
                    }

                    yield return null;
                }
            }

            IEnumerator SeatCoroutine()
            {
                while (true)
                {
                    SeatPoints.TryGetEmptyPoint(out var haveEmptySeatPoint, out var seatPoint);
                    if (!haveEmptySeatPoint) { yield return null; continue; }
                    
                    QueuePoints.GetFirstPoint(out var queuePoint);
                    queuePoint.GetCustomer(out var haveCustomer, out var customer);
                    if (!haveCustomer) { yield return null; continue; }
                    
                    seatPoint.SetCustomer(customer);
                    seatPoint.GetStandPoint(out var standPoint);
                    seatPoint.GetSitPoint(out var sitPoint);
                    
                    customer.WalkToSeatPoint(standPoint, sitPoint,
                        onArrive: () => Debug.Log($"{customer.name} 坐在 {seatPoint.name} 上了。"));
                    yield return null;
                }
            }
        }

        private void StopQueueCoroutine()
        {
            if (QueueCor is null) return;
            StopCoroutine(QueueCor);
            QueueCor = null;
        }
        
        private void StopSeatCoroutine()
        {
            if (SeatCor is null) return;
            StopCoroutine(SeatCor);
            SeatCor = null;
        }
    }
}