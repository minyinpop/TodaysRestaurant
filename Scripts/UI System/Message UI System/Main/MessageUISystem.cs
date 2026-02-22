using System;
using System.Collections.Generic;
using Common.Item.Data.Ingredient;
using Common.Value;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.System;
using UI_System.Message_UI_System.Child.Switch_UI_System.System;
using UI_System.Message_UI_System.Child.Tip_UI_System.System;
using UnityEngine;

namespace UI_System.Message_UI_System.Main
{
    public sealed class MessageUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Transform tipUISystemParent;
                                private static TipUISystem _tipUISystem;
        [field: SerializeField] private Transform switchUISystemParent;
                                private static SwitchUISystem _switchUISystem;
        [field: SerializeField] private Transform itemGetUISystemParent;
                                private static ItemGetUISystem _itemGetUISystem;

        private void Awake()
        {
            if (tipUISystemParent == null)
            {
                Debug.Log($"{nameof(MessageUISystem)} > {nameof(tipUISystemParent)} cannot be null.");
                return;
            }
            
            if (!tipUISystemParent.TryGetComponent(out _tipUISystem))
            {
                Debug.Log($"{nameof(MessageUISystem)} > {nameof(tipUISystemParent)} cannot get {nameof(TipUISystem)}.");
                return;
            }
            
            if (switchUISystemParent == null)
            {
                Debug.Log($"{nameof(MessageUISystem)} > {nameof(switchUISystemParent)} cannot be null.");
                return;
            }

            if (!switchUISystemParent.TryGetComponent(out _switchUISystem))
            {
                Debug.Log($"{nameof(MessageUISystem)} > {nameof(switchUISystemParent)} cannot get {nameof(SwitchUISystem)}.");
                return;
            }

            if (itemGetUISystemParent == null)
            {
                Debug.Log($"{nameof(MessageUISystem)} > {nameof(itemGetUISystemParent)} cannot be null.");
                return;
            }
            
            if (!itemGetUISystemParent.TryGetComponent(out _itemGetUISystem))
            {
                Debug.Log($"{nameof(MessageUISystem)} > {nameof(itemGetUISystemParent)} cannot get {nameof(ItemGetUISystem)}.");
            }
        }
        
        #region Message UI
            public static void ShowTipUI(PopUpUIContent content, Action onConfirm = null)
            {
                _tipUISystem.ShowTipUI(content, onConfirm);
            }
            
            public static void ShowSwitchUI(PopUpUIContent content, Action onConfirm, Action onCancel = null)
            {
                _switchUISystem.ShowUI(content, onConfirm, onCancel);
            }
            
            public static void ShowItemGetUI(PopUpUIContent content, List<IngredientSO> items, Action onConfirm)
            {
                _itemGetUISystem.ShowUI(content, items, onConfirm);
            }
        #endregion
    }
}