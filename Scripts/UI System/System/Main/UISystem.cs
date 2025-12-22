using System;
using System.Collections.Generic;
using Item;
using Item.Serving_Note;
using Player_System.System.Child;
using Player_System.System.Child.Mouse_System.Child;
using Player_System.System.Main;
using UI_System.System.Child;
using UI_System.System.Child.Hotbar_UI_System;
using UnityEngine;

namespace UI_System.System.Main
{
    public sealed class UISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private ItemDragUISystem itemDragUISystem;
        [field: SerializeField] private ServingNoteUISystem servingNoteUISystem;
        [field: SerializeField] private HotbarUISystem hotbarUISystem;
        
        private readonly Queue<Action> _activeActions = new();

        private void OnEnable()
        {
            #region PlayerSystem
                PlayerSystem.RightButtonClicked += ClickRightButton;
                _activeActions.Enqueue(() => PlayerSystem.RightButtonClicked -= ClickRightButton);
            #endregion
            
            #region ItemDragSystem
                ItemDragSystem.ItemDragUIRequired += RequireItemDragUI;
                _activeActions.Enqueue(() => ItemDragSystem.ItemDragUIRequired -= RequireItemDragUI);
            #endregion
            
            #region ServingNoteSO
                ServingNoteSO.ServingNoteUIRequired += RequireServingNoteUI;
                _activeActions.Enqueue(() => ServingNoteSO.ServingNoteUIRequired -= RequireServingNoteUI);
            #endregion
            
            #region HotbarUIRequired
                InventorySystem.HotbarUIRequired += RequireHotbarUI;
                _activeActions.Enqueue(() => InventorySystem.HotbarUIRequired -= RequireHotbarUI);
                
                InventorySystem.HotbarPerformed += PerformHotbar;
                _activeActions.Enqueue(() => InventorySystem.HotbarPerformed -= PerformHotbar);

                InventorySystem.TryItemAdded += TryAddItem;
                _activeActions.Enqueue(() => InventorySystem.TryItemAdded -= TryAddItem);
            #endregion
        }
        
        private void OnDisable()
        {
            while (_activeActions.Count > 0) _activeActions.Dequeue()?.Invoke();
        }
        
        #region PlayerSystem
            private void ClickRightButton()
            {
                hotbarUISystem.ClickRightButton();
            }
        #endregion

        #region ItemDragSystem
            private void RequireItemDragUI(bool isDragging, ItemSO item)
            {
                itemDragUISystem.RequiresUI(isDragging, item);
            }
        #endregion
        
        #region ServingNoteSO
            private void RequireServingNoteUI(ServingNoteSO servingNoteSO, GameObject prefab)
            {
                servingNoteUISystem.RequiresUI(servingNoteSO, prefab);
            }
        #endregion
        
        #region HotbarUIRequired
            private void RequireHotbarUI(bool show)
            {
                hotbarUISystem.RequiresUI(show);
            }

            private void PerformHotbar(int hotbarIndex)
            {
                hotbarUISystem.PerformHotbar(hotbarIndex);
            }
            
            private bool TryAddItem(ItemSO item)
            {
                return hotbarUISystem.TryAddItem(item);
            }
        #endregion
    }
}