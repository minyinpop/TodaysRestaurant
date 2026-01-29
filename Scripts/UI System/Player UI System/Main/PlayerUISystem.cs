using Common.Item;
using UI_System.Player_UI_System.Child.Backpack_UI_System;
using UI_System.Player_UI_System.Child.Backpack_UI_System.System;
using UI_System.Player_UI_System.Child.Hotbar_UI_System;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.System;
using UI_System.Player_UI_System.Child.Item_Drag_UI_System;
using UI_System.Player_UI_System.Child.Item_Drag_UI_System.System;
using UnityEngine;

namespace UI_System.Player_UI_System.Main
{
    public sealed class PlayerUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Transform hotbarUISystemParent;
                                private static HotbarUISystem _hotbarUISystem;
        [field: SerializeField] private Transform backpackUISystemParent;
                                private static BackpackUISystem _backpackUISystem;
        [field: SerializeField] private Transform itemDragUISystemParent;
                                private static ItemDragUISystem _itemDragUISystem;

        private void Awake()
        {
            if (hotbarUISystemParent == null)
            {
                Debug.Log($"{nameof(PlayerUISystem)} > {nameof(hotbarUISystemParent)} cannot be null.");
                return;
            }
            
            if (backpackUISystemParent == null)
            {
                Debug.Log($"{nameof(PlayerUISystem)} > {nameof(backpackUISystemParent)} cannot be null.");
                return;
            }
            
            if (itemDragUISystemParent == null)
            {
                Debug.Log($"{nameof(PlayerUISystem)} > {nameof(itemDragUISystemParent)} cannot be null.");
                return;
            }

            _hotbarUISystem = hotbarUISystemParent.GetComponent<HotbarUISystem>();
            _backpackUISystem = backpackUISystemParent.GetComponent<BackpackUISystem>();
            _itemDragUISystem = itemDragUISystemParent.GetComponent<ItemDragUISystem>();
        }
        
        #region Player
            public static void ClickRightButton()
            {
                _hotbarUISystem.UseSelectedHotbarSlotItem();
            }
        #endregion

        #region Inventory
            public static void PerformHotbar(int hotbarIndex)
            {
                _hotbarUISystem.PerformHotbar(hotbarIndex);
            }
            
            public static void PerformBackpack()
            {
                _backpackUISystem.PerformBackpack();
            }
            
            public static bool TryAddItem(ItemSO itemData)
            {
                return _hotbarUISystem.TryAddItem(itemData);
            }
            
            public static bool TryRemoveItem(ItemSO itemData)
            {
                return _hotbarUISystem.TryRemoveItem(itemData);
            }
        #endregion
        
        #region ItemDrag
            public static void RequireItemDragUI(bool isDragging, ItemSO item)
            {
                _itemDragUISystem.RequiresUI(isDragging, item);
            }
        #endregion
    }
}