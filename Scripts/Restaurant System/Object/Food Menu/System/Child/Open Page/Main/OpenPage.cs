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
using Message_System.System.Main;
using Player_System.Data.Main;
using Restaurant_System.Object.Food_Menu.Object;
using Restaurant_System.Object.Food_Menu.Object.Item_Slot.Base;
using Restaurant_System.Object.Food_Menu.Object.Item_Slot.Type.Select_Food_Slot;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child.Select_Food_Page;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child.Select_Food_Page.Data;
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
        [field: SerializeField] private MessageSystem MessageSystem;
        
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

        private readonly List<Action> ActiveActions = new();
        
        private FoodType CurrentFoodType = FoodType.Soup;

        public event Action OnClickOpenUIConfirmButton;

        private void OnEnable()
        {
            ConfirmButton.OnClick += OnConfirmButtonClicked;
            ConfirmButton.SetInteractable(true);
            ActiveActions.Add(() =>
            {
                ConfirmButton.SetInteractable(false);
                ConfirmButton.OnClick -= OnConfirmButtonClicked;
            });
            UnlockFoodPage.OnClick += OnUnlockFoodSlotClicked;
            SelectFoodPage.OnClick += OnSelectFoodSlotClicked;
        }
        
        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
            UnlockFoodPage.OnClick -= OnUnlockFoodSlotClicked;
            SelectFoodPage.OnClick -= OnSelectFoodSlotClicked;
        }

        public void Show()
        {
            UI.SetActive(true);
            
            // Unlock Food Page
            FindCategory(CurrentFoodType, out var foodCategory);
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
                ActiveActions.Add(() =>
                {
                    button_FoodTypeButton.SetInteractable(false);
                    button_FoodTypeButton.OnClick -= OnClicked;
                });
                button_FoodTypeButton.SetInteractable(true);
                continue;

                void OnClicked(FoodType foodType)
                {
                    CurrentFoodType = foodType;
                    FindCategory(CurrentFoodType, out var newFoodCategory);
                    
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
                    MessageSystem.ShowTipUI(
                        content: new PopUpUIContent(
                            message: "請選擇料理",
                            confirmButtonTitle: "確認",
                            cancelButtonTitle: string.Empty,
                            closeButtonTitle: string.Empty));
                    break;
                }
                case SelectFoodSlotType.UnFull:
                {
                    MessageSystem.ShowSwitchUI(
                        content: new PopUpUIContent(
                            message: "還有料理可以選擇\n要直接開始營業嗎？",
                            confirmButtonTitle: "開始營業",
                            cancelButtonTitle: "再想一下",
                            closeButtonTitle: string.Empty),
                        onConfirm: () => OnClickOpenUIConfirmButton?.Invoke());
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