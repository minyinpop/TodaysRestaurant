using System.Restaurant.Child.Customer.Main;
using UnityEngine;

namespace System.Restaurant.Child.Queue.Object
{
    internal sealed class QueuePoint : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform StandPoint;

        private CustomerSystem Customer;

        #region Status
            public void SetCustomer(CustomerSystem customer) => Customer = customer;
            public bool IsOccupied() => Customer is not null;
        #endregion
        
        #region Position
            public void GetStandPoint(out Transform point) => point = StandPoint;
        #endregion
    }
}