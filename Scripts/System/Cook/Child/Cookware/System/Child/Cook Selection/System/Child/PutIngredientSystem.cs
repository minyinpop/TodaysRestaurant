using System.Collections.Generic;
using System.General;
using System.Linq;
using System.Storage_Slot.Base;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using Data.Item.Type.Food;
using UnityEngine;

namespace System.Cook.Child.Cookware.System.Child.Cook_Selection.System.Child
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
                itemSlot_ItemSlot.Add(itemData);
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

        public void GetIngredients(out List<ItemSO> ingredients)
        {
            ingredients = new List<ItemSO>();
            foreach (var itemSlot in ItemSlots)
            {
                itemSlot.Get(out var item);
                ingredients.Add(item);
            }
        }
    }
}