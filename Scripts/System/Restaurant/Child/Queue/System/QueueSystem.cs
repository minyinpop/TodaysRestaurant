using System.Collections;
using System.Restaurant.Child.Customer.Main;
using Data.Restaurant.Queue_System;
using UnityEngine;

namespace System.Restaurant.Child.Queue.System
{
    internal sealed class QueueSystem : MonoBehaviour
    {
        [field: Header("Customer")]
        [field: SerializeField] private Transform SpawnPoint;
        [field: SerializeField] private Transform CustomerParent;
        [field: SerializeField] private GameObject CustomerPrefab;
        [field: SerializeField] private int CustomerAmountPerRound;
        [field: SerializeField] private int CustomerComeDuration;
        
        [field: Header("Queue")]
        [field: SerializeField] private QueueSystemSO QueueSystemData;
        [field: SerializeField] private Transform QueuePointParent;
        
        private int CurrentCustomerAmount;
        
        private IEnumerator QueueCor;

        private void Awake()
        {
            // Customer
            CustomerAmountPerRound = Mathf.Abs(CustomerAmountPerRound);
            CustomerComeDuration = Mathf.Abs(CustomerComeDuration);
            
            // Queue
            QueueSystemData.Init(QueuePointParent);
        }

        private void OnDisable()
        {
            StopSystem();
        }

        public void StartSystem()
        {
            QueueCor = QueueCoroutine();
            StartCoroutine(QueueCor);
            return;

            IEnumerator QueueCoroutine()
            {
                // TODO 先以 while 製作，之後再改成 Queue 有變更後呼叫
                while (CurrentCustomerAmount < CustomerAmountPerRound)
                {
                    QueueSystemData.TryGetEmptyQueuePoint(out var haveEmptyQueuePoint, out var queuePoint);
                    if (haveEmptyQueuePoint)
                    {
                        CurrentCustomerAmount += 1;

                        var customer = Instantiate(CustomerPrefab, queuePoint.transform.position, Quaternion.identity, CustomerParent);
                        var customer_CustomerSystem = customer.GetComponent<CustomerSystem>();

                        QueueSystemData.AddCustomer(queuePoint, customer_CustomerSystem);

                        customer_CustomerSystem.SetSkin();
                    }

                    yield return null;
                }
            }
        }

        private void StopSystem()
        {
            if (QueueCor is null) return;
            StopCoroutine(QueueCor);
            QueueCor = null;
        }
    }
}