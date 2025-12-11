using System.Collections;
using Economy_System.Child.Creature.Customer;
using Economy_System.Child.Creature.Customer.System.Main;
using Economy_System.Child.Restaurant_System.Object;
using UnityEngine;

namespace Economy_System.Child.Restaurant_System
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
        [field: SerializeField] private SeatPoint[] SeatPoints;
        
        private int CurrentCustomerAmount;
        
        private IEnumerator MainCor;

        private void Awake()
        {
            CustomerAmountPerRound = Mathf.Abs(CustomerAmountPerRound);
            CustomerComeDuration = Mathf.Abs(CustomerComeDuration);
        }

        private void OnDisable()
        {
            StopMainCoroutine();
        }

        public void StartSystem()
        {
            MainCor = MainCoroutine();
            StartCoroutine(MainCor);
            return;
            
            IEnumerator MainCoroutine()
            {
                while (CurrentCustomerAmount < CustomerAmountPerRound)
                {
                    foreach (var seatPoint in SeatPoints)
                    {
                        if (seatPoint.IsOccupied()) continue;
                        
                        CurrentCustomerAmount++;
                        var newCustomer = Instantiate(CustomerPrefab, CustomerSpawnPoint.position, Quaternion.identity, CustomerParent).GetComponent<CustomerSystem>();
                        
                        seatPoint.SetCustomer(newCustomer);
                        
                        seatPoint.GetStandPoint(out var standPoint);
                        seatPoint.GetSitPoint(out var sitPoint);
                        newCustomer.WalkToSeatPoint(standPoint, sitPoint);
                        
                        yield return new WaitForSeconds(CustomerComeDuration);
                    }

                    yield return null;
                }
            }
        }

        private void StopMainCoroutine()
        {
            if (MainCor is null) return;
            StopCoroutine(MainCor);
            MainCor = null;
        }
    }
}