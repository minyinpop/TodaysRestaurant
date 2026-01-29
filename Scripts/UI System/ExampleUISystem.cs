using System;
using System.Collections.Generic;
using Common.Item.Ingredient;
using Common.Value;
using UI_System.Child.Message_UI_System.System;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.System;
using UI_System.Message_UI_System.Child.Switch_UI_System.System;
using UI_System.Message_UI_System.Child.Tip_UI_System.System;
using UnityEngine;

namespace UI_System
{
    public sealed class ExampleUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Transform tipSystemParent;
                                private static TipUISystem _tipUISystem;
        [field: SerializeField] private Transform switchSystemParent;
                                private static SwitchUISystem _switchUISystem;
        [field: SerializeField] private Transform defeatSystemParent;
                                private static DefeatUISystem _defeatUISystem;
        [field: SerializeField] private Transform itemGetUISystemParent;
                                private static ItemGetUISystem _itemGetUISystem;
        [field: SerializeField] private Transform optionUISystemParent;
                                private static OptionUISystem _optionUISystem;

        private void Awake()
        {
            _tipUISystem = tipSystemParent?.GetComponent<TipUISystem>();
            _switchUISystem = switchSystemParent?.GetComponent<SwitchUISystem>();
            _defeatUISystem = defeatSystemParent?.GetComponent<DefeatUISystem>();
            _itemGetUISystem = itemGetUISystemParent?.GetComponent<ItemGetUISystem>();
            _optionUISystem = optionUISystemParent?.GetComponent<OptionUISystem>();
        }
        
        #region Message UI
            public static void ShowTipUI(PopUpUIContent content, Action onConfirm = null) =>
                _tipUISystem.ShowTipUI(content, onConfirm);
            public static void ShowSwitchUI(PopUpUIContent content, Action onConfirm, Action onCancel = null) =>
                _switchUISystem.ShowUI(content, onConfirm, onCancel);
            public static void ShowDefeatUI(PopUpUIContent content, Action onConfirm) =>
                _defeatUISystem.SpawnUI(content, onConfirm);
            public static void ShowItemGetUI(PopUpUIContent content, List<IngredientSO> items, Action onConfirm) =>
                _itemGetUISystem.ShowUI(content, items, onConfirm);
            public static void ShowOptionUI() =>
                _optionUISystem.SpawnUI();
        #endregion

        private static bool IsValid(MonoBehaviour target)
        {
            if (target == null)
                Debug.LogError($"UISystem > {target.name} is null, but you try to use it.");
            return target == null;
        }
    }
}