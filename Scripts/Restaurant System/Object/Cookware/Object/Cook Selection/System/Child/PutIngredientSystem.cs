using System;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Button;
using Common.Item_Slot.Legacy.Child;
using Common.Item.Data.Food;
using Common.Item.Data.Ingredient;
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
        
        private readonly List<PutIngredientSlot> slots = new();
        
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
                var itemSlot_ItemSlot = itemSlot.GetComponent<PutIngredientSlot>();
                slots.Add(itemSlot_ItemSlot);
                itemSlot_ItemSlot.TryAddItem(itemData);
            }
            
            // UI
            PutIngredientUI.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(PutIngredientUI_CanvasGroup, FadeInSettings,
                onComplete: () => SetInteractable(true));
        }

        public void Hide(Action onComplete = null)
        {
            SetInteractable(false);
            foreach (var action in CloseAction) action?.Invoke();
            CloseAction.Clear();
            
            DoAnimation.DoFade_CanvasGroup(PutIngredientUI_CanvasGroup, FadeOutSettings,
                onComplete: () =>
                {
                    PutIngredientUI.SetActive(false);
                    foreach (var itemSlot in slots) Destroy(itemSlot.gameObject);
                    slots.Clear();
                    onComplete?.Invoke();
                });
        }

        public void SetInteractable(bool interactable)
        {
            ConfirmButton.SetInteractable(interactable);
            CloseButton.SetInteractable(interactable);
        }
        
        public bool CheckRecipeIsCorrect()
        {
            return !slots.Where(itemSlot => itemSlot.IsEmpty()).Any();
        }

        public void GetIngredients(out Queue<IIngredient> ingredients)
        {
            ingredients = new Queue<IIngredient>();
            foreach (var itemSlot in slots)
            {
                itemSlot.TryGetItem(out var item);
                ingredients.Enqueue(item);
            }
        }
    }
}