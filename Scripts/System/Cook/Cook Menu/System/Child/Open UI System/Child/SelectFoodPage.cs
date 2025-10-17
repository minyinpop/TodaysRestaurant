using System.Collections.Generic;
using Data.General.Enum;
using Data.Item.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;

namespace System.Cook.Cook_Menu.System.Child.Open_UI_System.Child
{
    internal sealed class SelectFoodPage : MonoBehaviour
    {
        [field: Header("Select Food Slot")]
        [field: SerializeField] private Transform SelectFoodSlotParent;
        [field: SerializeField] private GameObject SelectFoodSlotPrefab;
        private readonly List<ItemSlot> SelectFoodSlots = new();
        private readonly List<Action> SelectFoodSlot_Actions = new();
        
        [field: Header("Develop Only")]
        [field: SerializeField, Range(0, 12)] private int UnlockSoupSlotIndex;
        [field: SerializeField, Range(0, 12)] private int UnlockDrinkSlotIndex;

        private const int TotalSlotCount = 12;

        private FoodType FoodType = FoodType.Soup;

        public event Action<ItemSlot, ItemSO> OnClick;
        
        private void OnDisable()
        {
            foreach (var action in SelectFoodSlot_Actions) action?.Invoke();
            SelectFoodSlot_Actions.Clear();
        }

        public void Spawn(FoodType foodType)
        {
            FoodType = foodType;
            switch (FoodType)
            {
                case FoodType.Soup:
                {
                    for (var i = 0; i < TotalSlotCount; i++)
                    {
                        var slot = Instantiate(SelectFoodSlotPrefab, SelectFoodSlotParent);
                        var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                        SelectFoodSlots.Add(slot_ItemSlot);
                        slot_ItemSlot.OnClick += OnClick;
                        SelectFoodSlot_Actions.Add(() => slot_ItemSlot.OnClick -= OnClick);
                        
                        if (i < UnlockSoupSlotIndex) { slot_ItemSlot.SetSlotState(ItemSlotState.NoItem); }
                        else if (i >= UnlockSoupSlotIndex) { slot_ItemSlot.SetSlotState(ItemSlotState.Lock); }
                    }
                    break;
                }
                // TODO 2025.10.18
                // case FoodType.Drink:
                // {
                //     for (var i = 0; i < UnlockDrinkSlotIndex; i++)
                //     {
                //         var slot = Instantiate(SelectFoodSlotPrefab, SelectFoodSlotParent);
                //         var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                //         SelectFoodSlots.Add(slot_ItemSlot);
                //     }
                //     break;
                // }
            }
        }

        public bool Add(ItemSO itemData)
        {
            foreach (var slot in SelectFoodSlots)
            {
                if (!slot.Add(itemData)) continue;
                return true;
            }

            return false;
        }

        public bool Remove(ItemSO unlockedSlotItemData)
        {
            foreach (var slot in SelectFoodSlots)
            {
                slot.Get(out var itemData);
                if (itemData is null) continue;
                if (itemData == unlockedSlotItemData) return true;
                slot.Add(itemData);
            }

            return false;
        }

        public void CancelSelect(ItemSlot slot)
        {
            slot.Reset();
        }
    }
}