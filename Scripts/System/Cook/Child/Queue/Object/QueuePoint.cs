using UnityEngine;

namespace System.Cook.Child.Queue.Object
{
    internal sealed class QueuePoint : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform StandPoint;

        private Customer.Object.Main.Customer Customer;

        public void GetStandPoint(out Transform point) => point = StandPoint;

        public void SetCustomer(Customer.Object.Main.Customer customer) => Customer = customer;
        
        public void IsOccupied(out bool isOccupied) => isOccupied = Customer is not null;
    }
}