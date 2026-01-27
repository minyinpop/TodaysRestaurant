using System;
using System.Collections.Generic;
using Common.Item;
using Common.Item.Ingredient;
using Common.Item.Serving_Note;
using Common.Value;
using UI_System.System.Child;
using UI_System.System.Child.Backpack_UI_System;
using UI_System.System.Child.Food_Menu_UI_System;
using UI_System.System.Child.Hotbar_UI_System;
using UI_System.System.Child.Message_UI_System.System;
using UI_System.System.Child.Serving_Note_UI_System;
using UI_System.System.Child.Switch_UI_System.System;
using UI_System.System.Child.Tip_UI_System.System;
using UnityEngine;

namespace UI_System.System.Main
{
    public sealed class UISystem : MonoBehaviour
    {
        [field: Header("Item Drag")]
        [field: SerializeField] private Transform itemDragUISystemParent;
                                private static ItemDragUISystem _itemDragUISystem;
        
        [field: Header("Serving Note")]
        [field: SerializeField] private Transform servingNoteUISystemParent;
                                private static ServingNoteUISystem _servingNoteUISystem;
        
        [field: Header("Hotbar")]
        [field: SerializeField] private Transform hotbarUISystemParent;
                                private static HotbarUISystem _hotbarUISystem;
        
        [field: Header("Backpack")]
        [field: SerializeField] private Transform backpackUISystemParent;
                                private static BackpackUISystem _backpackUISystem;
        
        [field: Header("Food Menu")]
        [field: SerializeField] private Transform foodMenuUISystemParent;
                                private static FoodMenuUISystem _foodMenuUISystem;
        
        [field: Header("Message")]
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
            _itemDragUISystem = itemDragUISystemParent?.GetComponent<ItemDragUISystem>();
        
            _servingNoteUISystem = servingNoteUISystemParent?.GetComponent<ServingNoteUISystem>();
        
            _hotbarUISystem = hotbarUISystemParent?.GetComponent<HotbarUISystem>();
            
            _backpackUISystem = backpackUISystemParent?.GetComponent<BackpackUISystem>();
        
            _foodMenuUISystem = foodMenuUISystemParent?.GetComponent<FoodMenuUISystem>();
            
            _tipUISystem = tipSystemParent?.GetComponent<TipUISystem>();
            _switchUISystem = switchSystemParent?.GetComponent<SwitchUISystem>();
            _defeatUISystem = defeatSystemParent?.GetComponent<DefeatUISystem>();
            _itemGetUISystem = itemGetUISystemParent?.GetComponent<ItemGetUISystem>();
            _optionUISystem = optionUISystemParent?.GetComponent<OptionUISystem>();
        }
        
        #region Player
            public static void ClickRightButton()
            {
                if (IsValid(_hotbarUISystem)) return;
                _hotbarUISystem.ClickRightButton();
            }
        #endregion

        #region Inventory
            public static void InitializeHotbarUI() =>
                _hotbarUISystem.Initialize();
            public static void PerformHotbar(int hotbarIndex) =>
                _hotbarUISystem.PerformHotbar(hotbarIndex);
            public static void PerformBackpack() =>
                _backpackUISystem.PerformBackpack();
            public static bool TryAddItem(ItemSO itemData) =>
                _hotbarUISystem.TryAddItem(itemData);
            public static bool TryRemoveItem(ItemSO itemData) =>
                _hotbarUISystem.TryRemoveItem(itemData);
        #endregion
        
        #region ItemDrag
            public static void RequireItemDragUI(bool isDragging, ItemSO item) =>
                _itemDragUISystem.RequiresUI(isDragging, item);
        #endregion
        
        #region ServingNote
            public static bool TryInitializeServingNoteUI(ServingNoteSO servingNoteData, GameObject prefab) =>
                _servingNoteUISystem.TryInitialize(servingNoteData, prefab);
            public static void ToggleServingNoteUI(ServingNoteSO servingNoteData) =>
                _servingNoteUISystem.ToggleUI(servingNoteData);
            public static void GetServingNoteItems(ServingNoteSO servingNoteData, out List<ItemSO> servingNoteItems) =>
                _servingNoteUISystem.GetServingNoteItems(servingNoteData, out servingNoteItems);
            public static void RemoveServingNoteUI(ServingNoteSO servingNoteData) =>
                _servingNoteUISystem.RemoveServingNoteUI(servingNoteData);
        #endregion

        #region Food Menu
            public static void SpawnFoodMenu(Action onConfirm) =>
                _foodMenuUISystem.SpawnUI(onConfirm);
            public static void DestroyFoodMenu() =>
                _foodMenuUISystem.DestroyUI();
        #endregion
        
        #region Message UI
            public static void ShowTipUI(PopUpUIContent content, Action onConfirm = null) =>
                _tipUISystem.SpawnUI(content, onConfirm);
            public static void ShowSwitchUI(PopUpUIContent content, Action onConfirm, Action onCancel = null) =>
                _switchUISystem.SpawnUI(content, onConfirm, onCancel);
            public static void ShowDefeatUI(PopUpUIContent content, Action onConfirm) =>
                _defeatUISystem.SpawnUI(content, onConfirm);
            public static void ShowItemGetUI(PopUpUIContent content, List<IngredientSO> items, Action onConfirm) =>
                _itemGetUISystem.SpawnUI(content, items, onConfirm);
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