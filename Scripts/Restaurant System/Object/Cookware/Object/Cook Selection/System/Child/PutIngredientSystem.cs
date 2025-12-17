using System;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Object;
using Common.Object.Storage_Slot;
using Item;
using Item.Data;
using Item.Data.Food;
using UnityEngine;

namespace Restaurant_System.Object.Cookware.Object.Cook_Selection.System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class PutIngredientSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("UI")]
        [field: SerializeField] private GameObject PutIngredientUI;
        [field: SerializeField] private CanvasGroup PutIngredientUI_CanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup FadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup FadeOutSettings;
        
        [field: Header("Button")]
        [field: SerializeField] private Button ConfirmButton;
        [field: SerializeField] private Button CloseButton;
        
        [field: Header("Item Slot")]
        [field: SerializeField] private GameObject ItemSlotPrefab;
        [field: SerializeField] private Transform ItemSlotParent;
        
        private readonly List<StorageSlot> ItemSlots = new();
        
        private readonly List<Action> CloseAction = new();

        private void OnDisable()
        {
            foreach (var action in CloseAction) action?.Invoke();
            CloseAction.Clear();
        }

        public void Show(FoodSO selectedFoodData, Action onConfirm, Action onCancel)
        {
            if (PutIngredientUI.activeSelf) return;
            
            // Button
            ConfirmButton.OnClick += onConfirm;
            CloseAction.Add(() => ConfirmButton.OnClick -= onConfirm);
            CloseButton.OnClick += onCancel;
            CloseAction.Add(() => CloseButton.OnClick -= onCancel);
            
            // Item Slot
            selectedFoodData.GetRecipeSheet(out var recipeSheet);
            foreach (var itemData in recipeSheet)
            {
                var itemSlot = Instantiate(ItemSlotPrefab, ItemSlotParent);
                var itemSlot_ItemSlot = itemSlot.GetComponent<StorageSlot>();
                ItemSlots.Add(itemSlot_ItemSlot);
                itemSlot_ItemSlot.TryAddItem(itemData, out _);
            }
            
            // UI
            PutIngredientUI.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(PutIngredientUI_CanvasGroup, FadeInSettings,
                onComplete: () => SetInteractable(true));
        }

        public void Hide(Action onComplete)
        {
            SetInteractable(false);
            foreach (var action in CloseAction) action?.Invoke();
            CloseAction.Clear();
            
            DoAnimation.DoFade_CanvasGroup(PutIngredientUI_CanvasGroup, FadeOutSettings,
                onComplete: () =>
                {
                    PutIngredientUI.SetActive(false);
                    foreach (var itemSlot in ItemSlots) Destroy(itemSlot.gameObject);
                    ItemSlots.Clear();
                    onComplete?.Invoke();
                });
        }

        public void SetInteractable(bool interactable)
        {
            ConfirmButton.SetInteractable(interactable);
            CloseButton.SetInteractable(interactable);
            foreach (var itemSlot in ItemSlots) itemSlot.SetInteractable(interactable);
        }
        
        public bool CheckRecipeIsCorrect()
        {
            return !ItemSlots.Where(itemSlot => itemSlot.IsEmpty()).Any();
        }

        public void GetIngredients(out Queue<ItemSO> ingredients)
        {
            ingredients = new Queue<ItemSO>();
            foreach (var itemSlot in ItemSlots)
            {
                itemSlot.GetItem(out var item);
                ingredients.Enqueue(item);
            }
        }
    }
}