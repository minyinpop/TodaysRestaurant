using System.Collections.Generic;
using System.Cook.Cook_Menu.System.Child.Open_UI_System.Child;
using System.Cook.Cook_Menu.System.Object;
using Data.Food.Food_Category.Base;
using Data.General.Enum;
using Data.Item.Base;
using Data.Player.Base;
using General.Object.Item_Slot.Base;
using UnityEngine;

namespace System.Cook.Cook_Menu.System.Child.Open_UI_System.Main
{
    internal sealed class OpenUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject UI;
        
        [field: Header("Child System")]
        [field: SerializeField] private UnlockFoodPage UnlockFoodPage;
        [field: SerializeField] private SelectFoodPage SelectFoodPage;
        
        [field: Header("Food Type Button")]
        [field: SerializeField] private Transform FoodTypeButtonParent;
        [field: SerializeField] private GameObject FoodTypeButtonPrefab;
        private readonly List<FoodTypeButton> FoodTypeButtons = new();
        private readonly List<Action> FoodTypeButton_Actions = new();
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private FoodType CurrentFoodType = FoodType.Soup; // Default is Soup.

        private void OnEnable()
        {
            UnlockFoodPage.OnClick += OnUnlockFoodSlotClicked;
            SelectFoodPage.OnClick += OnSelectFoodSlotClicked;
        }
        
        private void OnDisable()
        {
            UnlockFoodPage.OnClick -= OnUnlockFoodSlotClicked;
            SelectFoodPage.OnClick -= OnSelectFoodSlotClicked;
        }

        public void Open()
        {
            UI.SetActive(true);
            
            // Unlock Food Page
            FindCategory(CurrentFoodType, out var foodCategory);
            UnlockFoodPage.Spawn(foodCategory);
            
            // Select Dish Page
            SelectFoodPage.Spawn(CurrentFoodType);
            
            // Food Type Button
            PlayerData.GetUnlockFoods(out var dishCategory);
            foreach (var category in dishCategory)
            {
                category.GetValues(out var foodTypeData, out _);
                var button = Instantiate(FoodTypeButtonPrefab, FoodTypeButtonParent);
                var button_FoodTypeButton = button.GetComponent<FoodTypeButton>();
                FoodTypeButtons.Add(button_FoodTypeButton);
                button_FoodTypeButton.Init(foodTypeData);
                button_FoodTypeButton.OnClick += OnClicked;
                FoodTypeButton_Actions.Add(() =>
                {
                    button_FoodTypeButton.SetInteractable(false);
                    button_FoodTypeButton.OnClick -= OnClicked;
                });
                button_FoodTypeButton.SetInteractable(true);
                continue;

                void OnClicked(FoodType foodType)
                {
                    CurrentFoodType = foodType;
                    FindCategory(CurrentFoodType, out var foodCategory);
                    
                    // Unlock Food Page
                    UnlockFoodPage.Clear();
                    UnlockFoodPage.Spawn(foodCategory);
                    
                    // Select Food Page
                    SelectFoodPage.Clear();
                    SelectFoodPage.Spawn(CurrentFoodType);
                }
            }
        }

        #region On Item Slot Clicked
            private void OnUnlockFoodSlotClicked(ItemSlot slot, ItemSO itemData)
            {
                slot.GetSlotState(out var slotState);
                switch (slotState)
                {
                    case ItemSlotState.Select:
                    {
                        if (SelectFoodPage.Remove(itemData)) UnlockFoodPage.ChangeSelectState(slot);
                        break;
                    }
                    case ItemSlotState.UnSelect:
                    {
                        if (SelectFoodPage.Add(itemData)) UnlockFoodPage.ChangeSelectState(slot);
                        break;
                    }
                }
            }

            private void OnSelectFoodSlotClicked(ItemSlot slot, ItemSO itemData)
            {
                SelectFoodPage.CancelSelect(slot);
                UnlockFoodPage.CancelSelect(itemData);
            }
        #endregion

        #region Tools
            private void FindCategory(FoodType targetFoodType, out FoodCategorySO targetFoodCategory)
            {
                PlayerData.GetUnlockFoods(out var foodCategory);
                foreach (var category in foodCategory)
                {
                    category.GetValues(out var foodTypeData, out _);
                    foodTypeData.GetValues(out var foodType, out _, out _);
                    if (foodType != targetFoodType) continue;
                    targetFoodCategory = category;
                    return;
                }
                
                targetFoodCategory = null;
            }
        #endregion
    }
}