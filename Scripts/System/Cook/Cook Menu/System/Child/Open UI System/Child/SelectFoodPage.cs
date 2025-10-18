using System.Collections.Generic;
using Data.Cook.Select_Food_Type;
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
        
        [field: Header("Select Food Type Data")]
        [field: SerializeField] private SelectFoodTypeSO SelectSoupTypeData;
        [field: SerializeField] private SelectFoodTypeSO SelectDrinkTypeData;
        
        [field: Header("Develop Only")]
        [field: SerializeField, Range(0, 12)] private int UnlockSoupSlotIndex;
        [field: SerializeField, Range(0, 12)] private int UnlockDrinkSlotIndex;

        private const int TotalSlotCount = 12;

        private FoodType CurrentFoodType = FoodType.Soup;
        private SelectFoodTypeSO CurrentSelectFoodTypeSO;

        public event Action<ItemSlot, ItemSO> OnClick;
        
        private void OnDisable()
        {
            foreach (var action in SelectFoodSlot_Actions) action?.Invoke();
            SelectFoodSlot_Actions.Clear();
        }

        public void Spawn(FoodType foodType)
        {
            CurrentFoodType = foodType;
            switch (CurrentFoodType)
            {
                case FoodType.Soup:
                {
                    CurrentSelectFoodTypeSO = SelectSoupTypeData;
                    CurrentSelectFoodTypeSO.Clear();
                    CurrentSelectFoodTypeSO.Init(UnlockSoupSlotIndex);
                    
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
                case FoodType.Drink:
                {
                    CurrentSelectFoodTypeSO = SelectDrinkTypeData;
                    CurrentSelectFoodTypeSO.Clear();
                    CurrentSelectFoodTypeSO.Init(UnlockSoupSlotIndex);
                    
                    for (var i = 0; i < TotalSlotCount; i++)
                    {
                        var slot = Instantiate(SelectFoodSlotPrefab, SelectFoodSlotParent);
                        var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                        SelectFoodSlots.Add(slot_ItemSlot);
                        
                        slot_ItemSlot.OnClick += OnClick;
                        SelectFoodSlot_Actions.Add(() => slot_ItemSlot.OnClick -= OnClick);
                        
                        if (i < UnlockDrinkSlotIndex) { slot_ItemSlot.SetSlotState(ItemSlotState.NoItem); }
                        else if (i >= UnlockDrinkSlotIndex) { slot_ItemSlot.SetSlotState(ItemSlotState.Lock); }
                    }

                    break;
                }
            }
        }

        public void Clear()
        {
            foreach (var action in SelectFoodSlot_Actions) action?.Invoke();
            SelectFoodSlot_Actions.Clear();
            foreach (var slot in SelectFoodSlots) Destroy(slot.gameObject);
            SelectFoodSlots.Clear();
        }

        public void Add(ItemSO targetItemData)
        {
            foreach (var slot in SelectFoodSlots)
            {
                slot.Add(targetItemData, out var isSuccess);
                if (!isSuccess) continue;
                CurrentSelectFoodTypeSO.Add(targetItemData);
                return;
            }
        }

        public void Remove(ItemSO targetItemData)
        {
            foreach (var slot in SelectFoodSlots)
            {
                slot.Get(out var itemData);
                if (itemData is null) continue;
                if (itemData != targetItemData) continue;
                slot.Reset();
                return;
            }
        }

        public void CancelSelect(ItemSlot slot)
        {
            slot.Reset();
        }
    }
}