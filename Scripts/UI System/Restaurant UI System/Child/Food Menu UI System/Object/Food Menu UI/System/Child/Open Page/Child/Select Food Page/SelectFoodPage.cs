using System;
using System.Collections.Generic;
using Common.Item.Data;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Base;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Type.Select_Food_Slot;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page.Data;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page
{
    internal sealed class SelectFoodPage : MonoBehaviour
    {
        [field: Header("頁面狀態")]
        [field: SerializeField] private bool interactable;
        
        [field: Header("Select Food Slot")]
        [field: SerializeField] private Transform SelectFoodSlotParent;
        [field: SerializeField] private GameObject SelectFoodSlotPrefab;
        
        [field: Header("Select Food Type Data")]
        [field: SerializeField] private SelectFoodPageSO SelectFoodPageData;
        
        private readonly List<ItemSlot> _selectFoodSlots = new();
        public IReadOnlyList<ItemSlot> SelectFoodSlots => _selectFoodSlots;
        
        private readonly List<Action> SelectFoodSlot_Actions = new();
        
        // Develop Only
        private const int TotalSlotCount = 16;
        private const int UnlockSlotCount = 2;
        
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
                
                slot_ItemSlot.Interactable = interactable;
                
                _selectFoodSlots.Add(slot_ItemSlot);
                
                slot_ItemSlot.OnClick += OnClicked;
                SelectFoodSlot_Actions.Add(() => slot_ItemSlot.OnClick -= OnClicked);

                slot_ItemSlot.SetSlotState(i >= UnlockSlotCount ? ItemSlotState.Lock : ItemSlotState.NoItem);
            }
        }

        public void Add(ItemSO targetItemData, out bool isSuccess)
        {
            for (var i = 0; i < TotalSlotCount; i++)
            {
                var slot = _selectFoodSlots[i];
                slot.Add(targetItemData, out isSuccess);
                if (!isSuccess) continue;
                SelectFoodPageData.AddItemData(i, targetItemData);
                return;
            }
            
            isSuccess = false;
        }

        public void Remove(ItemSO targetItemData)
        {
            foreach (var slot in _selectFoodSlots)
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
                var slot = _selectFoodSlots[i];
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

        public void SetInteractable(bool interactable)
        {
            this.interactable = interactable;
            
            foreach (var slot in _selectFoodSlots)
            {
                slot.Interactable = this.interactable;
            }
        }
    }
}