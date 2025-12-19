using Item.Serving_Note;
using Restaurant_System.Object.Creature.Customer.System.Main;
using UnityEngine;

namespace Restaurant_System.Object
{
    internal sealed class SeatPoint : MonoBehaviour
    {
        #region Customer
            private Customer currentCustomer;
            public void SetCustomer(Customer customer) => currentCustomer = customer;
        #endregion
        
        #region Position
            [field: Header("Point")]
            [field: SerializeField] private Transform standPoint;
            [field: SerializeField] private Transform sitPoint;
            public Transform StandPoint() => standPoint;
            public Transform SitPoint() => sitPoint;
        #endregion
        
        #region Status
            public bool IsOccupied() => currentCustomer is not null;
        #endregion
        
        #region Data
            [field: Header("Data")]
            [field: SerializeField] private ServingNoteSO servingNoteData;
        #endregion
    }
}