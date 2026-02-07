using Common.Data.Item.Serving_Note;
using Restaurant_System.Object.Creature.Customer.System.Main;
using UnityEngine;

namespace Restaurant_System.Object
{
    public sealed class SeatPoint : MonoBehaviour
    {
        [field: Header("Seat Point")]
        [field: SerializeField] private Transform standPoint;
        [field: SerializeField] private Transform sitPoint;
        
        [field: Header("Serving Note")]
        [field: SerializeField] private ServingNoteSO servingNoteData;

        private Customer _currentCustomer;
        
        #region Position
            public Transform StandPoint() => standPoint;
            public Transform SitPoint() => sitPoint;
        #endregion
        
        #region Serving Note
            public ServingNoteSO ServingNote() => servingNoteData;
        #endregion
        
        #region Customer
            public void SetCustomer(Customer customer) => _currentCustomer = customer;
        #endregion
        
        #region Status
            public bool IsOccupied() => _currentCustomer is not null;
        #endregion

        public void Reset()
        {
            servingNoteData.Reset();
            _currentCustomer = null;
        }
    }
}