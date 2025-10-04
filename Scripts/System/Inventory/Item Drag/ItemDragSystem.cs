using System.Collections.Generic;
using System.Input.Main;
using System.Linq;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace System.Inventory.Item_Drag
{
    internal sealed class ItemDragSystem : MonoBehaviour
    {
        [field: Header("Drag UI")]
        [field: SerializeField] private GameObject DragUIPrefab;
        [field: SerializeField] private Transform DragUIParent;
        
        [field: Header("Event System")]
        [field: SerializeField] private EventSystem EventSystem;
        [field: SerializeField] private string ItemSlotTag;
        
        private GameObject DragUI;
        private ItemDragUI DragUIScript;
        
        private ItemSO DraggedItemData;

        private void OnEnable()
        {
            InputSystem.MouseLeftButtonClicked += OnPointerClick;
        }
        
        private void OnDisable()
        {
            InputSystem.MouseLeftButtonClicked -= OnPointerClick;
        }

        private void OnPointerClick()
        {
            TryGetItemSlot(out var slot);
            if (slot is null) return;
            if (DraggedItemData is null)
            {
                TryToTakeItem();
            }
            else
            {
                var currentItemSlotScript = slot.GetComponent<ItemSlot>();
                if (currentItemSlotScript.IsEmpty()) PutItemToEmptySlot(currentItemSlotScript);
                else SwitchItem(currentItemSlotScript);
            }

            return;

            void TryGetItemSlot(out GameObject clickedSlot)
            {
                InputSystem.GetMousePosition(out var position);
                var pointer = new PointerEventData(EventSystem)
                {
                    position = position
                };
                var results = new List<RaycastResult>();
                EventSystem.RaycastAll(pointer, results);
                foreach (var result in results.Where(result => result.gameObject.CompareTag(ItemSlotTag)))
                {
                    clickedSlot = result.gameObject;
                    return;
                }

                clickedSlot = null;
            }
            
            void TryToTakeItem()
            {
                slot.GetComponent<ItemSlot>().Get(out var item);
                if (item is null) return;
                DraggedItemData = item;
                DragUI = Instantiate(DragUIPrefab, DragUIParent);
                DragUIScript = DragUI.GetComponent<ItemDragUI>();
                DraggedItemData.GetItemSprite(out var sprite);
                DragUIScript.SetSprite(sprite);
            }

            void PutItemToEmptySlot(ItemSlot currentItemSlotScript)
            {
                if (!currentItemSlotScript.Add(DraggedItemData)) return;
                Destroy(DragUI);
                DragUI = null;
                DragUIScript = null;
                DraggedItemData = null;
            }

            void SwitchItem(ItemSlot currentItemSlotScript)
            {
                currentItemSlotScript.Get(out var item);
                currentItemSlotScript.Add(DraggedItemData);
                DraggedItemData = item;
                DraggedItemData.GetItemSprite(out var sprite);
                DragUIScript.SetSprite(sprite);
            }
        }
    }
}