using System.Collections.Generic;
using System.Cook.Cook_Menu.System.Object;
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
        private readonly List<ItemSlot> SelectDishSlots = new();
        
        [field: Header("Food Type Button")]
        [field: SerializeField] private Transform FoodTypeButtonParent;
        [field: SerializeField] private GameObject FoodTypeButtonPrefab;
        private readonly List<FoodTypeButton> FoodTypeButtons = new();
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private FoodType CurrentFoodType = FoodType.Soup; // Default is Soup.
        
        private readonly List<Action> ActiveActions = new();

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }
        

        public void Open()
        {
            UI.SetActive(true);
            PlayerData.GetUnlockedDishes(out var dishCategory);
            
            // Dish Type Button
            foreach (var category in dishCategory)
            {
                category.GetValues(out var foodTypeData, out _);
                var button = Instantiate(FoodTypeButtonPrefab, FoodTypeButtonParent);
                var button_FoodTypeButton = button.GetComponent<FoodTypeButton>();
                FoodTypeButtons.Add(button_FoodTypeButton);
                button_FoodTypeButton.Init(foodTypeData);
                button_FoodTypeButton.OnClick += OnClicked;
                ActiveActions.Add(() =>
                {
                    button_FoodTypeButton.OnClick -= OnClicked;
                    button_FoodTypeButton.SetInteractable(false);
                });
                button_FoodTypeButton.SetInteractable(true);
                return;

                void OnClicked(FoodType foodType)
                {
                    Debug.Log(foodType);
                }
            }

            // Unlock Dish Page
            foreach (var category in dishCategory)
            {
                category.GetValues(out var foodTypeData, out var dishesData);
                foodTypeData.GetValues(out var foodType, out _, out _);
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
            
            // Select Dish Page
            for (var i = 0; i < 12; i++)
            {
                var slot = Instantiate(i > 2 ? SelectDishSlotPrefab_01 : SelectDishSlotPrefab_02, SelectDishSlotParent);
                var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                SelectDishSlots.Add(slot_ItemSlot);
            }
        }
    }
}