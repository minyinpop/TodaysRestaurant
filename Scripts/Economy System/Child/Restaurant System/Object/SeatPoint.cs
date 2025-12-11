using Economy_System.Child.Creature.Customer;
using Economy_System.Child.Creature.Customer.System.Main;
using UnityEngine;

namespace Economy_System.Child.Restaurant_System.Object
{
    internal sealed class SeatPoint : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform StandPoint;
        [field: SerializeField] private Transform SitPoint;

        private CustomerSystem Customer;
        
        #region Status
            public void SetCustomer(CustomerSystem customer)
            {
                Customer = customer;
            }
                
            public void GetCustomer(out bool haveCustomer, out CustomerSystem customer)
            {
                haveCustomer = IsOccupied();
                customer = IsOccupied() ? Customer : null;
                if (IsOccupied()) Customer = null;
            }
                
            public bool IsOccupied()
            {
                return Customer is not null;
            }
        #endregion
        
        #region Position
            public void GetStandPoint(out Transform point)
            {
                point = StandPoint;
            }
            
            public void GetSitPoint(out Transform point)
            {
                point = SitPoint;
            }
        #endregion
    }
}