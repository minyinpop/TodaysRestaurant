using System.Collections.Generic;
using Data.General.Enum;
using Data.Item.Base;
using Data.Player;
using General.Object.Item_Slot.Base;
using UnityEngine;

namespace System.Cook.Cook_Menu.System.Child
{
    internal sealed class OpenUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject UI;
        
        [field: Header("Unlock Dish Slot")]
        [field: SerializeField] private Transform UnlockDishSlotParent;
        [field: SerializeField] private GameObject UnlockDishSlotPrefab;
        private readonly List<ItemSlot> UnlockDishSlots = new();
        
        [field: Header("Select Dish Slot")]
        [field: SerializeField] private Transform SelectDishSlotParent;
        [field: SerializeField] private GameObject SelectDishSlotPrefab_01;
        [field: SerializeField] private GameObject SelectDishSlotPrefab_02;
        [field: SerializeField] private GameObject SelectDishSlotPrefab_03;
        
        [field: Header("Dish Type Button")]
        [field: SerializeField] private Transform DishTypeButtonParent;
        [field: SerializeField] private GameObject DishTypeButtonPrefab;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private FoodType CurrentFoodType = FoodType.Soup;
        
        private readonly List<Action> ActiveActions = new();

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }
        

        public void Open()
        {
            UI.SetActive(true);
            PlayerData.GetUnlockedDishes(out var dishes);
            foreach (var dish in dishes)
            {
                dish.GetValues(out var foodType, out var dishesData);
                if (foodType != CurrentFoodType) continue;
                foreach (var dishData in dishesData)
                {
                    var slot = Instantiate(UnlockDishSlotPrefab, UnlockDishSlotParent);
                    var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                    UnlockDishSlots.Add(slot_ItemSlot);
                    slot_ItemSlot.Add(dishData);
                    slot_ItemSlot.OnClick += OnClicked;
                    ActiveActions.Add(() =>
                    {
                        slot_ItemSlot.OnClick -= OnClicked;
                        slot_ItemSlot.SetInteractable(false);
                    });
                    slot_ItemSlot.SetInteractable(true);
                    continue;

                    void OnClicked(bool onSelect, ItemSO itemData)
                    {
                        // TODO 檢查是否可以再增加料理到右側
                        slot_ItemSlot.SetAlpha();
                    }
                }
            }
        }
    }
}