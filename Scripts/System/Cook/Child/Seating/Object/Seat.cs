using UnityEngine;

namespace System.Cook.Child.Seating.Object
{
    internal sealed class Seat : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform SitPoint;

        private GameObject Customer;

        public void Set(GameObject customer) { Customer = customer; }
        
        public void Check(out bool isOccupied) { isOccupied = Customer is not null; }
    }
}