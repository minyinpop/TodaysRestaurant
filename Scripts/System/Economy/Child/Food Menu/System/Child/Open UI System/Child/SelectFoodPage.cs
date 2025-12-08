using System.Collections.Generic;
using System.Economy.Child.Food_Menu.Object.Item_Slot.Base;
using Data.Economy.Food_Menu;
using Data.Economy.Food_Menu.Select_Food_Page;
using Data.Item.Abstract;
using UnityEngine;

namespace System.Economy.Child.Food_Menu.System.Child.Open_UI_System.Child
{
    internal sealed class SelectFoodPage : MonoBehaviour
    {
        [field: Header("Select Food Slot")]
        [field: SerializeField] private Transform SelectFoodSlotParent;
        [field: SerializeField] private GameObject SelectFoodSlotPrefab;
        private readonly List<ItemSlot> SelectFoodSlots = new();
        private readonly List<Action> SelectFoodSlot_Actions = new();
        
        [field: Header("Select Food Type Data")]
        [field: SerializeField] private SelectFoodPageSO SelectFoodPageData;

        // Develop Only
        private const int TotalSlotCount = 12;
        private const int UnlockSlotCount = 3;
        
        public event Action<ItemSlot, ItemSO> OnClick;

        private void Awake()
        {
            SelectFoodPageData.Init(TotalSlotCount);
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
                SelectFoodPageData.AddItemData(i, targetItemData);
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
                SelectFoodPageData.RemoveItemData(itemData);
                slot.Reset();
                return;
            }
        }

        public void CancelSelect(ItemSlot slot)
        {
            slot.Get(out var itemData);
            SelectFoodPageData.RemoveItemData(itemData);
            slot.Reset();
        }

        public void IsAllSlotsHaveItemData(out SelectItemSlotType type)
        {
            var isAllSelect = true;
            type = SelectItemSlotType.UnSelect;
            for (var i = 0; i < TotalSlotCount; i++)
            {
                var slot = SelectFoodSlots[i];
                if (i >= UnlockSlotCount) continue;
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