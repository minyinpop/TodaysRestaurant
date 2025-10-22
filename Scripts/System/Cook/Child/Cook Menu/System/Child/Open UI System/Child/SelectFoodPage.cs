using System.Collections.Generic;
using System.Item_Slot.Base;
using Data.Cook;
using Data.Cook.Select_Food_Type;
using Data.Item.Base;
using UnityEngine;

namespace System.Cook.Child.Cook_Menu.System.Child.Open_UI_System.Child
{
    internal sealed class SelectFoodPage : MonoBehaviour
    {
        [field: Header("Select Food Slot")]
        [field: SerializeField] private Transform SelectFoodSlotParent;
        [field: SerializeField] private GameObject SelectFoodSlotPrefab;
        private readonly List<ItemSlot> SelectFoodSlots = new();
        private readonly List<Action> SelectFoodSlot_Actions = new();
        
        [field: Header("Select Food Type Data")]
        [field: SerializeField] private SelectFoodTypeSO SelectFoodTypeData;

        // Develop Only
        private const int TotalSlotCount = 12;
        private const int UnlockSlotCount = 3;
        
        public event Action<ItemSlot, ItemSO> OnClick;

        private void Awake()
        {
            SelectFoodTypeData.Init(TotalSlotCount);
        }

        private void OnDisable()
        {
            foreach (var action in SelectFoodSlot_Actions) action?.Invoke();
            SelectFoodSlot_Actions.Clear();
        }

        public void Spawn()
        {
            for (var i = 0; i < TotalSlotCount; i++)
            {
                var slot = Instantiate(SelectFoodSlotPrefab, SelectFoodSlotParent);
                var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                SelectFoodSlots.Add(slot_ItemSlot);
                
                slot_ItemSlot.OnClick += OnClick;
                SelectFoodSlot_Actions.Add(() => slot_ItemSlot.OnClick -= OnClick);

                slot_ItemSlot.SetSlotState(i >= UnlockSlotCount ? ItemSlotState.Lock : ItemSlotState.NoItem);
            }
        }

        public void Add(ItemSO targetItemData, out bool isSuccess)
        {
            for (var i = 0; i < TotalSlotCount; i++)
            {
                var slot = SelectFoodSlots[i];
                slot.Add(targetItemData, out isSuccess);
                if (!isSuccess) continue;
                SelectFoodTypeData.Add(i, targetItemData);
                return;
            }
            
            isSuccess = false;
        }

        public void Remove(ItemSO targetItemData)
        {
            foreach (var slot in SelectFoodSlots)
            {
                slot.Get(out var itemData);
                if (itemData is null) continue;
                if (itemData != targetItemData) continue;
                SelectFoodTypeData.Remove(itemData);
                slot.Reset();
                return;
            }
        }

        public void CancelSelect(ItemSlot slot)
        {
            slot.Get(out var itemData);
            SelectFoodTypeData.Remove(itemData);
            slot.Reset();
        }

        public void IsAllSlotsHaveItemData(out SelectItemSlotType type)
        {
            var isAllSelect = true;
            type = SelectItemSlotType.UnSelect;
            foreach (var slot in SelectFoodSlots)
            {
                slot.Get(out var itemData);
                if (itemData is null)
                {
                    isAllSelect = false;
                    continue;
                }

                type = SelectItemSlotType.UnFull;
            }

            if (isAllSelect) type = SelectItemSlotType.Full;
        }
    }
}