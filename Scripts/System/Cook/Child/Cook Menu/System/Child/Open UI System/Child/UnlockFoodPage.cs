using System.Collections.Generic;
using Data.Food.Food_Category.Base;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;

namespace System.Cook.Child.Cook_Menu.System.Child.Open_UI_System.Child
{
    internal sealed class UnlockFoodPage : MonoBehaviour
    {
        [field: Header("Unlock Dish Slot")]
        [field: SerializeField] private Transform UnlockFoodSlotParent;
        [field: SerializeField] private GameObject UnlockFoodSlotPrefab;
        private readonly List<ItemSlot> UnlockFoodSlots = new();
        private readonly List<Action> UnlockFoodSlot_Actions = new();

        public event Action<ItemSlot, ItemSO> OnClick;
        
        private void OnDisable()
        {
            foreach (var action in UnlockFoodSlot_Actions) action?.Invoke();
            UnlockFoodSlot_Actions.Clear();
        }

        public void Spawn(FoodCategorySO foodCategory)
        {
            foodCategory.GetValues(out _, out var dishesData);
            foreach (var dishData in dishesData)
            {
                var slot = Instantiate(UnlockFoodSlotPrefab, UnlockFoodSlotParent);
                var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                UnlockFoodSlots.Add(slot_ItemSlot);
                slot_ItemSlot.Add(dishData);
                
                slot_ItemSlot.OnClick += OnClick;
                UnlockFoodSlot_Actions.Add(() => slot_ItemSlot.OnClick -= OnClick);
                
                slot_ItemSlot.SetSlotState(ItemSlotState.UnSelect);
            }
        }

        public void Clear()
        {
            foreach (var action in UnlockFoodSlot_Actions) action?.Invoke();
            UnlockFoodSlot_Actions.Clear();
            foreach (var slot in UnlockFoodSlots) Destroy(slot.gameObject);
            UnlockFoodSlots.Clear();
        }
        
        public void ChangeSelectState(ItemSlot itemSlot)
        {
            itemSlot.ChangeSelectState();
            itemSlot.SetAlpha();
        }

        public void CheckItemDataHasBeenSelect(ItemSO targetItemData)
        {
            foreach (var slot in UnlockFoodSlots)
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
            foreach (var slot in UnlockFoodSlots)
            {
                slot.Get(out var itemData); 
                if (targetItemData != itemData) continue;
                slot.ChangeSelectState();
                slot.SetAlpha();
                return;
            }
        }
    }
}