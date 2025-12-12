using Restaurant_System.Object.Creature.Customer.System.Main;
using UnityEngine;

namespace Restaurant_System.System.Child.Customer_Manager_System.Object
{
    internal sealed class SeatPoint : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private Transform StandPoint;
        [field: SerializeField] private Transform SitPoint;

        private Customer Customer;
        
        #region Status
            public void SetCustomer(Customer customer)
            {
                Customer = customer;
            }
                
            public void GetCustomer(out bool haveCustomer, out Customer customer)
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