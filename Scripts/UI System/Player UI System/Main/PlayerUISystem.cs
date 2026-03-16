using Common.Item.Data;
using UI_System.Player_UI_System.Child.Backpack_UI_System.System;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.System;
using UI_System.Player_UI_System.Child.Item_Drag_UI_System.System;
using UnityEngine;

namespace UI_System.Player_UI_System.Main
{
    public sealed class PlayerUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private HotbarUISystem hotbarUISystem;
                                private static HotbarUISystem _hotbarUISystem;
        [field: SerializeField] private BackpackUISystem backpackUISystem;
                                private static BackpackUISystem _backpackUISystem;
        [field: SerializeField] private ItemDragUISystem itemDragUISystem;
                                private static ItemDragUISystem _itemDragUISystem;

        private void Awake()
        {
            if (hotbarUISystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(hotbarUISystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (backpackUISystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(backpackUISystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (itemDragUISystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(backpackUISystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            _hotbarUISystem = hotbarUISystem;
            _backpackUISystem = backpackUISystem;
            _itemDragUISystem = itemDragUISystem;
        }

        #region Player
            public static void ClickRightButton()
            {
                _hotbarUISystem.UseSelectedHotbarSlotItem();
            }
        #endregion

        #region Inventory
            public static void SetHotbarUI(bool isEnabled)
            {
                _hotbarUISystem.SetHotbarUI(isEnabled);
            }

            public static void SetBackpackUI(bool isEnabled)
            {
                _backpackUISystem.SetBackpackUI(isEnabled);
            }

            public static void RequireBackpackUI()
            {
                _backpackUISystem.RequireBackpackUI();
            }

            public static void PerformHotbar(int hotbarIndex)
            {
                _hotbarUISystem.PerformHotbar(hotbarIndex);
            }
            
            public static bool TryAddItem(IItem itemData)
            {
                return _hotbarUISystem.TryAddItem(itemData);
            }
            
            public static bool TryRemoveItem(ItemSO itemData)
            {
                return _hotbarUISystem.TryRemoveItem(itemData);
            }
        #endregion
        
        #region ItemDrag
            public static void RequireItemDragUI(bool isDragging, IItem item)
            {
                _itemDragUISystem.RequiresUI(isDragging, item);
            }
        #endregion
    }
}