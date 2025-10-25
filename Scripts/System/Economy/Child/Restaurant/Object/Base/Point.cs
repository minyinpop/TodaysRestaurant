using System.Economy.Child.Customer.Main;
using UnityEngine;

namespace System.Economy.Child.Restaurant.Object.Base
{
    internal abstract class Point : MonoBehaviour
    {
        #region Status
            public abstract void SetCustomer(CustomerSystem customer);
            public abstract void GetCustomer(out bool haveCustomer, out CustomerSystem customer);
            public abstract bool IsOccupied();
        #endregion
        
        #region Position
            public virtual void GetStandPoint(out Transform point) => point = null;
            public virtual void GetSitPoint(out Transform point) => point = null;
        #endregion
    }
}