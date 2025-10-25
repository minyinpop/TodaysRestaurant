using System.Restaurant.Child.Customer.Main;
using UnityEngine;

namespace System.Restaurant.Child.Seating.Object
{
    internal sealed class Seat : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform SitPoint;

        private CustomerSystem Customer;

        #region Status
            public void SetCustomer(CustomerSystem customer) => Customer = customer;
            public void IsOccupied(out bool isOccupied) => isOccupied = Customer is not null;
        #endregion
    }
}