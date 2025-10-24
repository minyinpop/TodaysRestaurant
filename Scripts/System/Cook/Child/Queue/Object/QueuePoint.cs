using UnityEngine;

namespace System.Cook.Child.Queue.Object
{
    internal sealed class QueuePoint : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform StandPoint;

        private GameObject Customer;

        public void Set(GameObject customer) { Customer = customer; }
        
        public void Check(out bool isOccupied) { isOccupied = Customer is not null; }
    }
}