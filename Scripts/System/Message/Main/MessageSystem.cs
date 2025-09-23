using System.Collections.Generic;
using System.Message.Child;
using Data.Item.Base;
using UnityEngine;

namespace System.Message.Main
{
    internal sealed class MessageSystem : MonoBehaviour
    {
        #region TipSystem
            [field: SerializeField] private TipSystem TipSystem;

            public void ShowTipUI(string message, Action onConfirm)
            {
                TipSystem.Show(message, onConfirm);
            }
        #endregion
        
        #region SwitchSystem
            [field: SerializeField] private SwitchSystem SwitchSystem;

            public void ShowSwitchUI(string message, Action onShow, Action onConfirm, Action onCancel, Action onClose)
            {
                SwitchSystem.Show(message, onShow, onConfirm, onCancel, onClose);
            }
        #endregion
        
        #region ItemGetSystem
            [field: SerializeField] private ItemGetSystem ItemGetSystem;
            [field: SerializeField] private List<ItemSO> TempItems;

            public void ShowItemGetUI()
            {
                ItemGetSystem.Show(TempItems);
            }
        #endregion
    }
}