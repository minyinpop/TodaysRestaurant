using System;
using System.Collections.Generic;
using Common.Item.Data;
using Common.Item.Data.Food.Data.Food_Category;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Base;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child
{
    internal sealed class UnlockFoodPage : MonoBehaviour
    {
        [field: Header("頁面狀態")]
        [field: SerializeField] private bool interactable;
        
        [field: Header("Unlock Dish Slot")]
        [field: SerializeField] private Transform UnlockFoodSlotParent;
        [field: SerializeField] private GameObject UnlockFoodSlotPrefab;
        
        private readonly List<ItemSlot> _unlockFoodSlots = new();
        public IReadOnlyList<ItemSlot> UnlockFoodSlots => _unlockFoodSlots;
        
        private readonly List<Action> _unlockFoodSlot_Actions = new();

        public event Action<ItemSlot, ItemSO> OnClicked;
        
        private void OnDisable()
        {
            foreach (var action in _unlockFoodSlot_Actions) action?.Invoke();
            _unlockFoodSlot_Actions.Clear();
        }

        public void Spawn(FoodCategorySO foodCategory)
        {
            foreach (var dishData in foodCategory.FoodsData)
            {
                var slot = Instantiate(UnlockFoodSlotPrefab, UnlockFoodSlotParent);
                var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                
                slot_ItemSlot.Interactable = interactable;
                
                _unlockFoodSlots.Add(slot_ItemSlot);
                slot_ItemSlot.Add(dishData);
                
                slot_ItemSlot.OnClick += OnClicked;
                _unlockFoodSlot_Actions.Add(() =>
                {
                    slot_ItemSlot.OnClick -= OnClicked;
                });
                
                slot_ItemSlot.SetSlotState(ItemSlotState.UnSelect);
            }
        }

        public void Clear()
        {
            foreach (var action in _unlockFoodSlot_Actions) action?.Invoke();
            _unlockFoodSlot_Actions.Clear();
            foreach (var slot in _unlockFoodSlots) Destroy(slot.gameObject);
            _unlockFoodSlots.Clear();
        }
        
        public void ChangeSelectState(ItemSlot itemSlot)
        {
            itemSlot.ChangeSelectState();
            itemSlot.SetAlpha();
        }

        public void CheckItemDataHasBeenSelect(ItemSO targetItemData)
        {
            foreach (var slot in _unlockFoodSlots)
            {
                slot.Get(out var itemData);
                if (targetItemData != itemData) continue;
                slot.SetSlotState(ItemSlotState.Select);
                slot.SetAlpha();
                return;
            }
        }

        public void CancelSelect(ItemSO targetItemData)
        {
            foreach (var slot in _unlockFoodSlots)
            {
                slot.Get(out var itemData); 
                if (targetItemData != itemData) continue;
                slot.ChangeSelectState();
                slot.SetAlpha();
                return;
            }
        }

        public void SetInteractable(bool interactable)
        {
            this.interactable = interactable;
            
            foreach (var slot in _unlockFoodSlots)
            {
                slot.Interactable = this.interactable;
            }
        }
    }
}