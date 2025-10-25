using System.Economy.Child.Customer.Main;
using System.Economy.Child.Restaurant.Object.Base;
using UnityEngine;

namespace System.Economy.Child.Restaurant.Object.Type
{
    internal sealed class SeatPoint : Point
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform StandPoint;
        [field: SerializeField] private Transform SitPoint;

        private CustomerSystem Customer;
        
        #region Status
            public override void SetCustomer(CustomerSystem customer)
            {
                Customer = customer;
            }
                
            public override void GetCustomer(out bool haveCustomer, out CustomerSystem customer)
            {
                haveCustomer = IsOccupied();
                customer = IsOccupied() ? Customer : null;
                if (IsOccupied()) Customer = null;
            }
                
            public override bool IsOccupied()
            {
                return Customer is not null;
            }
        #endregion
        
        #region Position
            public override void GetStandPoint(out Transform point)
            {
                point = StandPoint;
            }
            
            public override void GetSitPoint(out Transform point)
            {
                point = SitPoint;
            }
        #endregion
    }
}