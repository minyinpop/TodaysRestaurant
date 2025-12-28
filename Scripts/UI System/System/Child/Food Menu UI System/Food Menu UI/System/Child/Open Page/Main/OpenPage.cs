using System;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Object;
using Common.Value;
using Common.Value.Type;
using Item;
using Item.Food.Data.Food_Category;
using Player_System.Data.Main;
using Restaurant_System.Object.Food_Menu.Object;
using Restaurant_System.Object.Food_Menu.Object.Item_Slot.Base;
using Restaurant_System.Object.Food_Menu.Object.Item_Slot.Type.Select_Food_Slot;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child.Select_Food_Page;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child.Select_Food_Page.Data;
using UI_System.System.Main;
using UnityEngine;

namespace Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Main
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class OpenPage : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject UI;
        [field: SerializeField] private CanvasGroup UI_CanvasGroup;
        
        [field: Header("Child System")]
        [field: SerializeField] private UnlockFoodPage UnlockFoodPage;
        [field: SerializeField] private SelectFoodPage SelectFoodPage;
        
        [field: Header("Button")]
        [field: SerializeField] private Button ConfirmButton;
        
        [field: Header("Animation")]
        [field: SerializeField] private DoAnimation DoAnimation;
        [field: SerializeField] private DoFade_CanvasGroup ShowSettings;
        
        [field: Header("Food Type Button")]
        [field: SerializeField] private Transform FoodTypeButtonParent;
        [field: SerializeField] private GameObject FoodTypeButtonPrefab;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;
        [field: SerializeField] private SelectFoodPageSO SelectFoodPageData;

        private readonly List<Action> _cleanUpActions = new();
        
        private FoodType _currentFoodType = FoodType.Soup;

        public event Action OnClickOpenUIConfirmButton;

        private void OnEnable()
        {
            ConfirmButton.onClick += OnConfirmButtonClicked;
            ConfirmButton.SetInteractable(true);
            _cleanUpActions.Add(() =>
            {
                ConfirmButton.SetInteractable(false);
                ConfirmButton.onClick -= OnConfirmButtonClicked;
            });
            UnlockFoodPage.OnClick += OnUnlockFoodSlotClicked;
            SelectFoodPage.OnClick += OnSelectFoodSlotClicked;
        }
        
        private void OnDisable()
        {
            foreach (var action in _cleanUpActions) action?.Invoke();
            _cleanUpActions.Clear();
            UnlockFoodPage.OnClick -= OnUnlockFoodSlotClicked;
            SelectFoodPage.OnClick -= OnSelectFoodSlotClicked;
        }

        public void Show()
        {
            UI.SetActive(true);
            
            // Unlock Food Page
            FindCategory(_currentFoodType, out var foodCategory);
            UnlockFoodPage.Spawn(foodCategory);
            
            // Select Dish Page
            SelectFoodPage.Spawn();
            
            // Food Type Button
            PlayerData.GetUnlockFoods(out var dishCategory);
            foreach (var category in dishCategory)
            {
                category.GetValues(out var foodTypeData, out _);
                var button = Instantiate(FoodTypeButtonPrefab, FoodTypeButtonParent);
                var button_FoodTypeButton = button.GetComponent<FoodTypeButton>();
                button_FoodTypeButton.Init(foodTypeData);
                button_FoodTypeButton.OnClick += OnClicked;
                _cleanUpActions.Add(() =>
                {
                    button_FoodTypeButton.SetInteractable(false);
                    button_FoodTypeButton.OnClick -= OnClicked;
                });
                button_FoodTypeButton.SetInteractable(true);
                continue;

                void OnClicked(FoodType foodType)
                {
                    _currentFoodType = foodType;
                    FindCategory(_currentFoodType, out var newFoodCategory);
                    
                    // Unlock Food Page
                    UnlockFoodPage.Clear();
                    UnlockFoodPage.Spawn(newFoodCategory);

                    SelectFoodPageData.GetAllItemData(out var itemsData);
                    foreach (var itemData in itemsData.Where(itemData => itemData is not null)) { UnlockFoodPage.CheckItemDataHasBeenSelect(itemData); }
                }
            }
        }
        
        public void Hide(Action onComplete)
        {
            DoAnimation.DoFade_CanvasGroup(UI_CanvasGroup, ShowSettings,
                onComplete: OnComplete);
            return;

            void OnComplete()
            {
                UI.SetActive(false);
                onComplete?.Invoke();
            }
        }

        private void OnConfirmButtonClicked()
        {
            SelectFoodPage.IsAllSlotsHaveItemData(out var type);
            switch (type)
            {
                case SelectFoodSlotType.UnSelect:
                {
                    var content = new PopUpUIContent(
                        message: "請選擇料理",
                        confirmButtonTitle: "確認",
                        cancelButtonTitle: string.Empty,
                        closeButtonTitle: string.Empty);
                    UISystem.ShowTipUI(content);
                    break;
                }
                case SelectFoodSlotType.UnFull:
                {
                    var content = new PopUpUIContent(
                        message: "還有料理可以選擇\n要直接開始營業嗎？",
                        confirmButtonTitle: "開始營業",
                        cancelButtonTitle: "再想一下",
                        closeButtonTitle: string.Empty);
                    UISystem.ShowSwitchUI(
                        content: content,
                        onConfirm: OnClickOpenUIConfirmButton);
                    break;
                }
                case SelectFoodSlotType.Full:
                {
                    OnClickOpenUIConfirmButton?.Invoke();
                    break;
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
                        SelectFoodPage.Remove(itemData);
                        UnlockFoodPage.ChangeSelectState(slot);
                        break;
                    }
                    case ItemSlotState.UnSelect:
                    {
                        SelectFoodPage.Add(itemData, out var isSuccess);
                        if (isSuccess) UnlockFoodPage.ChangeSelectState(slot);
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