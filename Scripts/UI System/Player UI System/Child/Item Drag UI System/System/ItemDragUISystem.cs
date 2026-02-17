using Common.Item.Data;
using Input_System;
using UI_System.Player_UI_System.Child.Item_Drag_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Item_Drag_UI_System.System
{
    public sealed class ItemDragUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private ItemDragUI itemDragUI;
        [field: SerializeField] private RectTransform itemDragUIRect;
        
        private bool _isDragging;

        private void Awake()
        {
            if (itemDragUI == null)
            {
                Debug.Log($"{nameof(ItemDragUISystem)} > {nameof(itemDragUI)} > cannot be null.");
                return;
            }
            
            if (itemDragUIRect == null)
            {
                Debug.Log($"{nameof(ItemDragUISystem)} > {nameof(itemDragUIRect)} > cannot be null.");
            }
        }

        private void FixedUpdate()
        {
            if (_isDragging)
            {
                itemDragUIRect.anchoredPosition = InputSystem.MousePosition;
            }
        }

        public void RequiresUI(bool isDragging, IItem item)
        {
            _isDragging = isDragging;
            
            if (_isDragging)
            {
                itemDragUI.SetItemImage(item.ItemSprite);
                itemDragUIRect.anchoredPosition = InputSystem.MousePosition;
                itemDragUI.gameObject.SetActive(true);
            }
            else
            {
                itemDragUI.gameObject.SetActive(false);
                itemDragUI.ClearItemImage();
            }
        }
    }
}