using System.Collections.Generic;
using UnityEngine;

namespace System.Cook.Child.Customer.System
{
    internal sealed class CustomerSystem : MonoBehaviour
    {
        [field: SerializeField] private Transform SpawnPoint;
        [field: SerializeField] private Transform CustomerParent;
        [field: SerializeField] private GameObject CustomerPrefab;
        
        private readonly List<GameObject> Customers = new();

        public void Spawn(out GameObject customer)
        { 
            customer = Instantiate(CustomerPrefab, SpawnPoint.position, SpawnPoint.rotation, CustomerParent);
            Customers.Add(customer);
        }
    }
}