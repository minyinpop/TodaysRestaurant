using System.Collections.Generic;
using System.Message.System.Child;
using Data.General;
using Data.Item.Type.Ingredient;
using UnityEngine;

namespace System.Message.System.Main
{
    internal sealed class MessageSystem : MonoBehaviour
    {
        #region TipSystem
        [field: SerializeField] private TipSystem TipSystem;

        public void ShowTipUI(PopUpUIContent content, Action onConfirm = null)
        {
            TipSystem?.Show(content, onConfirm);
        }
        #endregion
        
        #region SwitchSystem
        [field: SerializeField] private SwitchSystem SwitchSystem;

        public void ShowSwitchUI(PopUpUIContent content, Action onShow = null, Action onConfirm = null, Action onCancel = null, Action onClose = null)
        {
            SwitchSystem?.Show(content, onShow, onConfirm, onCancel, onClose);
        }
        #endregion
        
        #region ItemGetSystem
        [field: SerializeField] private ItemGetSystem ItemGetSystem;
        [field: SerializeField] private List<IngredientSO> TempItems;

        public void ShowItemGetUI(PopUpUIContent content, Action onConfirm = null)
        {
            ItemGetSystem?.Show(content, TempItems, onConfirm);
        }
        #endregion
        
        #region DefeatSystem
        [field: SerializeField] private DefeatSystem DefeatSystem;

        public void ShowDefeatUI(PopUpUIContent content, Action onConfirm = null)
        {
            DefeatSystem?.Show(content, onConfirm);
        }
        #endregion
        
        #region OptionSystem
        [field: SerializeField] private OptionSystem OptionSystem;

        public void ShowOptionUI(PopUpUIContent content, Action onClose = null)
        {
            OptionSystem?.Show(content, onClose);
        }
        #endregion
    }
}