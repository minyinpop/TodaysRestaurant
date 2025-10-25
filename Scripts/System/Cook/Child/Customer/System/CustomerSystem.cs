using System.Collections.Generic;
using UnityEngine;

namespace System.Cook.Child.Customer.System
{
    internal sealed class CustomerSystem : MonoBehaviour
    {
        [field: SerializeField] private Transform SpawnPoint;
        [field: SerializeField] private Transform CustomerParent;
        [field: SerializeField] private GameObject CustomerPrefab;
        
        private readonly List<Object.Main.Customer> Customers = new();

        public void SpawnCustomer(out Object.Main.Customer customer)
        {
            customer = Instantiate(CustomerPrefab, SpawnPoint.position, SpawnPoint.rotation, CustomerParent).GetComponent<Object.Main.Customer>();
            Customers.Add(customer);
        }
    }
}