using System;
using Common.Value;
using Item;
using Item.Serving_Note;
using Message_System.System.Child;
using UI_System.System.Child;
using UI_System.System.Child.Food_Menu_UI_System;
using UI_System.System.Child.Hotbar_UI_System;
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
        
        [field: Header("Food Menu")]
        [field: SerializeField] private Transform foodMenuUISystemParent;
        private static FoodMenuUISystem _foodMenuUISystem;
        
        [field: Header("Message UI")]
        [field: SerializeField] private Transform tipSystemParent;
        private static TipSystem _tipSystem;
        [field: SerializeField] private Transform switchSystemParent;
        private static SwitchSystem _switchSystem;

        private void Awake()
        {
            _itemDragUISystem = itemDragUISystemParent.GetComponent<ItemDragUISystem>();
        
            _servingNoteUISystem = servingNoteUISystemParent.GetComponent<ServingNoteUISystem>();
        
            _hotbarUISystem = hotbarUISystemParent.GetComponent<HotbarUISystem>();
        
            _foodMenuUISystem = foodMenuUISystemParent.GetComponent<FoodMenuUISystem>();
            
            _tipSystem = tipSystemParent.GetComponent<TipSystem>();
            _switchSystem = switchSystemParent.GetComponent<SwitchSystem>();
        }
        
        #region Player
            public static void ClickRightButton() =>
                _hotbarUISystem?.ClickRightButton();
        #endregion

        #region Inventory
            public static void RequireHotbarUI() =>
                _hotbarUISystem.RequiresUI();
            public static void PerformHotbar(int hotbarIndex) =>
                _hotbarUISystem.PerformHotbar(hotbarIndex);
            public static bool TryAddItem(ItemSO item) =>
                _hotbarUISystem.TryAddItem(item);
        #endregion
        
        #region ItemDrag
            public static void RequireItemDragUI(bool isDragging, ItemSO item) =>
                _itemDragUISystem.RequiresUI(isDragging, item);
        #endregion
        
        #region ServingNote
            public static void RequireServingNoteUI(ServingNoteSO servingNoteSO, GameObject prefab) =>
                _servingNoteUISystem.RequiresUI(servingNoteSO, prefab);
        #endregion

        #region Message UI
            public static void ShowTipUI(PopUpUIContent content, Action onConfirm = null) =>
                _tipSystem?.Show(content, onConfirm);
            public static void ShowSwitchUI(PopUpUIContent content, Action onShow = null, Action onConfirm = null, Action onCancel = null, Action onClose = null) =>
                _switchSystem?.Show(content, onShow, onConfirm, onCancel, onClose);
        #endregion

        #region Food Menu
            public static void ShowFoodMenu(Action onComplete)
            {
                _foodMenuUISystem.RequiresUI(onComplete);
            }
        #endregion
    }
}