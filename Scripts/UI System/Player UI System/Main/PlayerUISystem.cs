using Common.Item;
using UI_System.Player_UI_System.Child.Backpack_UI_System.System;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.System;
using UI_System.Player_UI_System.Child.Item_Drag_UI_System.System;
using UnityEngine;

namespace UI_System.Player_UI_System.Main
{
    public sealed class 
        PlayerUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private HotbarUISystem hotbarUISystem;
                                private static HotbarUISystem _hotbarUISystem;
        [field: SerializeField] private BackpackUISystem backpackUISystem;
                                private static BackpackUISystem _backpackUISystem;
        [field: SerializeField] private ItemDragUISystem itemDragUISystem;
                                private static ItemDragUISystem _itemDragUISystem;

        private void Awake()
        {
            if (hotbarUISystem == null)
            {
                Debug.Log($"{nameof(PlayerUISystem)} > {nameof(hotbarUISystem)} cannot be null.");
                return;
            }
            else
            {
                _hotbarUISystem = hotbarUISystem;
            }
            
            if (backpackUISystem == null)
            {
                Debug.Log($"{nameof(PlayerUISystem)} > {nameof(backpackUISystem)} cannot be null.");
                return;
            }
            else
            {
                _backpackUISystem = backpackUISystem;
            }
            
            if (itemDragUISystem == null)
            {
                Debug.Log($"{nameof(PlayerUISystem)} > {nameof(itemDragUISystem)} cannot be null.");
            }
            else
            {
                _itemDragUISystem = itemDragUISystem;
            }
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
            
            public static void RequireBackpackUI()
            {
                _backpackUISystem.RequireBackpackUI();
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