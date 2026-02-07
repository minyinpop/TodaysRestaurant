using System;
using System.Collections.Generic;
using Common.Data.Item;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Base;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Type.Select_Food_Slot;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page.Data;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page
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
        
        public event Action<ItemSlot, ItemSO> OnClicked;

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
                
                slot_ItemSlot.OnClick += OnClicked;
                SelectFoodSlot_Actions.Add(() => slot_ItemSlot.OnClick -= OnClicked);

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

        public void IsAllSlotsHaveItemData(out SelectFoodSlotType type)
        {
            var isAllSelect = true;
            type = SelectFoodSlotType.UnSelect;
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

                type = SelectFoodSlotType.UnFull;
            }

            if (isAllSelect) type = SelectFoodSlotType.Full;
        }
    }
}