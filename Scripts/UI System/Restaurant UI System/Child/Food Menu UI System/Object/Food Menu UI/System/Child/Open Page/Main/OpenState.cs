using System;
using System.Collections.Generic;
using System.Linq;
using Common.Button;
using Common.Item.Data;
using Common.Item.Data.Food.Data.Food_Category;
using Common.Player.Child.Player_Unlock_Food;
using Common.Value;
using Common.Value.Type;
using UI_System.Message_UI_System.Main;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Base;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object.Item_Slot.Type.Select_Food_Slot;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page.Data;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Main
{
    internal sealed class OpenState : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject ui;
        
        [field: Header("Child System")]
        [field: SerializeField] private UnlockFoodPage unlockFoodPage;
        [field: SerializeField] private SelectFoodPage selectFoodPage;
        
        [field: Header("Button")]
        [field: SerializeField] private Button confirmButton;
        
        [field: Header("Food Type Button")]
        [field: SerializeField] private Transform foodTypeButtonParent;
        [field: SerializeField] private GameObject foodTypeButtonPrefab;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerUnlockFoodSO playerUnlockFoodData;
        [field: SerializeField] private SelectFoodPageSO selectFoodPageData;
        
        private FoodType _currentFoodType = FoodType.Soup;
        
        private readonly Queue<Action> _foodTypeButtonCleanupActions = new();
        public event Action OnConfirm;

        private void Awake()
        {
            // Book
            confirmButton.OnClick += OnClickConfirmButton;
            
            // Page
            unlockFoodPage.OnClicked += OnUnlockFoodSlotClicked;
            selectFoodPage.OnClicked += OnSelectFoodSlotClicked;
        }

        private void OnEnable()
        {
            confirmButton.SetInteractable(true);
        }

        private void OnDisable()
        {
            confirmButton.SetInteractable(false);
        }

        private void OnDestroy()
        {
            while (_foodTypeButtonCleanupActions.Count > 0) _foodTypeButtonCleanupActions.Dequeue()?.Invoke();
            
            // Book
            confirmButton.OnClick -= OnClickConfirmButton;
            
            // Page
            unlockFoodPage.OnClicked -= OnUnlockFoodSlotClicked;
            selectFoodPage.OnClicked -= OnSelectFoodSlotClicked;
        }

        public void Show()
        {
            ui.SetActive(true);
            
            // Unlock Food Page
            FindCategory(_currentFoodType, out var foodCategory);
            unlockFoodPage.Spawn(foodCategory);
            
            // Select Dish Page
            selectFoodPage.Spawn();
            
            // Food Type Button
            playerUnlockFoodData.GetUnlockFoods(out var dishCategory);
            foreach (var category in dishCategory)
            {
                category.GetValues(out var foodTypeData, out _);
                var button = Instantiate(foodTypeButtonPrefab, foodTypeButtonParent);
                var button_FoodTypeButton = button.GetComponent<FoodTypeButton>();
                button_FoodTypeButton.Init(foodTypeData);
                
                button_FoodTypeButton.OnClick += OnClicked;
                button_FoodTypeButton.SetInteractable(true);
                _foodTypeButtonCleanupActions.Enqueue(() =>
                {
                    button_FoodTypeButton.SetInteractable(false);
                    button_FoodTypeButton.OnClick -= OnClicked;
                });
                continue;

                void OnClicked(FoodType foodType)
                {
                    _currentFoodType = foodType;
                    FindCategory(_currentFoodType, out var newFoodCategory);
                    
                    // Unlock Food Page
                    unlockFoodPage.Clear();
                    unlockFoodPage.Spawn(newFoodCategory);

                    selectFoodPageData.GetAllItemData(out var itemsData);
                    foreach (var itemData in itemsData.Where(itemData => itemData is not null)) { unlockFoodPage.CheckItemDataHasBeenSelect(itemData); }
                }
            }
        }

        private void OnClickConfirmButton()
        {
            selectFoodPage.IsAllSlotsHaveItemData(out var type);
            if (type == SelectFoodSlotType.UnSelect)
            {
                var content = new PopUpUIContent(
                    message: "請選擇料理",
                    confirmButtonTitle: "確認",
                    cancelButtonTitle: string.Empty,
                    closeButtonTitle: string.Empty);
                MessageUISystem.ShowTipUI(content);
            }
            else if (type == SelectFoodSlotType.UnFull)
            {
                var content = new PopUpUIContent(
                    message: "還有料理可以選擇\n要直接開始營業嗎？",
                    confirmButtonTitle: "開始營業",
                    cancelButtonTitle: "再想一下",
                    closeButtonTitle: string.Empty);
                MessageUISystem.ShowSwitchUI(
                    content: content,
                    onConfirm: OnConfirm);
            }
            else if (type == SelectFoodSlotType.Full)
            {
                OnConfirm();
            }

            return;
            
            void OnConfirm()
            {
                if (this.OnConfirm == null)
                {
                    Debug.LogWarning($"{gameObject.name} > OpenState > OnConfirmButtonClicked > OnConfirmButtonClicked");
                    gameObject.SetActive(false);
                    return;
                }

                this.OnConfirm.Invoke();
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
                        selectFoodPage.Remove(itemData);
                        unlockFoodPage.ChangeSelectState(slot);
                        break;
                    }
                    case ItemSlotState.UnSelect:
                    {
                        selectFoodPage.Add(itemData, out var isSuccess);
                        if (isSuccess) unlockFoodPage.ChangeSelectState(slot);
                        break;
                    }
                }
            }

            private void OnSelectFoodSlotClicked(ItemSlot slot, ItemSO itemData)
            {
                selectFoodPage.CancelSelect(slot);
                unlockFoodPage.CancelSelect(itemData);
            }
        #endregion

        #region Tools
            private void FindCategory(FoodType targetFoodType, out FoodCategorySO targetFoodCategory)
            {
                playerUnlockFoodData.GetUnlockFoods(out var foodCategory);
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