using System.Collections.Generic;
using System.Message.Child;
using Data.General;
using Data.Item.Base;
using UnityEngine;
using Utage;

namespace System.Message.Main
{
    internal sealed class MessageSystem : MonoBehaviour
    {
        #region TipSystem
        [field: SerializeField] private TipSystem TipSystem;

        public void ShowTipUI(PopUpUIContent content, Action onConfirm)
        {
            TipSystem?.Show(content, onConfirm);
        }
        #endregion
        
        #region SwitchSystem
        [field: SerializeField] private SwitchSystem SwitchSystem;

        public void ShowSwitchUI(PopUpUIContent content, Action onShow, Action onConfirm, Action onCancel, Action onClose)
        {
            SwitchSystem?.Show(content, onShow, onConfirm, onCancel, onClose);
        }
        #endregion
        
        #region ItemGetSystem
        [field: SerializeField] private ItemGetSystem ItemGetSystem;
        [field: SerializeField] private List<ItemSO> TempItems;

        public void ShowItemGetUI(PopUpUIContent content, Action onConfirm)
        {
            ItemGetSystem?.Show(content, TempItems, onConfirm);
        }
        #endregion
        
        #region DefeatSystem
        [field: SerializeField] private DefeatSystem DefeatSystem;

        public void ShowDefeatUI(PopUpUIContent content, Action onConfirm)
        {
            DefeatSystem?.Show(content, onConfirm);
        }
        #endregion
        
        #region OptionSystem
        [field: SerializeField] private OptionSystem OptionSystem;

        public void ShowOptionUI(PopUpUIContent content, Action onClose)
        {
            OptionSystem?.Show(content, onClose);
        }
        #endregion
    }
}