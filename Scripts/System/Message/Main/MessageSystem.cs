using System.Collections.Generic;
using System.Message.Child;
using Data.Item.Base;
using UnityEngine;

namespace System.Message.Main
{
    internal sealed class MessageSystem : MonoBehaviour
    {
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