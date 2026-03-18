using System;
using System.Collections.Generic;
using Common.Item.Data;
using Common.Item.Data.Ingredient;
using Common.Value;
using UI_System.Message_UI_System.Child.Defeat_UI_System.System;
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
        [field: SerializeField] private Transform defeatUISystemParent;
                                private static DefeatUISystem _defeatUISystem;

        private void Awake()
        {
            #region 提示 UI
                if (tipUISystemParent is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(tipUISystemParent)} cannot be null.)");
                    Destroy(gameObject);
                    return;
                }
                
                if (!tipUISystemParent.TryGetComponent(out _tipUISystem))
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(tipUISystemParent)} cannot get {nameof(_tipUISystem.GetType)}.");
                    Destroy(gameObject);
                    return;
                }
            #endregion
            
            #region 選擇 UI
                if (switchUISystemParent is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(switchUISystemParent)} cannot be null.)");
                    Destroy(gameObject);
                    return;
                }
                
                if (!switchUISystemParent.TryGetComponent(out _switchUISystem))
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(switchUISystemParent)} cannot get {nameof(_switchUISystem.GetType)}.");
                    Destroy(gameObject);
                    return;
                }
            #endregion

            #region 獲得物品 UI
                if (itemGetUISystemParent is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(itemGetUISystemParent)} cannot be null.)");
                    Destroy(gameObject);
                    return;
                }
                    
                if (!itemGetUISystemParent.TryGetComponent(out _itemGetUISystem))
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(itemGetUISystemParent)} cannot get {nameof(_itemGetUISystem.GetType)}.");
                    Destroy(gameObject);
                    return;
                }
            #endregion
            
            #region 戰敗 UI
                if (defeatUISystemParent is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(defeatUISystemParent)} cannot be null.)");
                    Destroy(gameObject);
                    return;
                }
                        
                if (!defeatUISystemParent.TryGetComponent(out _defeatUISystem))
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(defeatUISystemParent)} cannot get {nameof(_defeatUISystem.GetType)}.");
                    Destroy(gameObject);
                }
            #endregion
        }
        
        public static void ShowTipUI(PopUpUIContent content, Action onConfirm = null)
        {
            _tipUISystem.ShowUI(content, onConfirm);
        }
        
        public static void ShowSwitchUI(PopUpUIContent content, Action onConfirm, Action onCancel = null)
        {
            _switchUISystem.ShowUI(content, onConfirm, onCancel);
        }
        
        public static void ShowItemGetUI(PopUpUIContent content, IItem[] items, Action onConfirm)
        {
            _itemGetUISystem.ShowUI(content, items, onConfirm);
        }

        public static void ShowDefeatUI(PopUpUIContent content, Action onConfirm)
        {
            _defeatUISystem.ShowUI(content, onConfirm);
        }
    }
}