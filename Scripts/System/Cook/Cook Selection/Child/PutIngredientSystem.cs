using System.Collections.Generic;
using System.General;
using System.Linq;
using System.Message.Main;
using Data.Animation.DOTween.Basic;
using Data.General;
using Data.Item.Type.Dish;
using DG.Tweening;
using General.Object;
using General.Object.Item_Slot.Base;
using UnityEngine;

namespace System.Cook.Cook_Selection.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class PutIngredientSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        [field: SerializeField] private MessageSystem MessageSystem;
        
        [field: Header("UI")]
        [field: SerializeField] private GameObject UIObject;
        [field: SerializeField] private CanvasGroup UICanvasGroup;
        [field: SerializeField] private Button ConfirmButton;
        [field: SerializeField] private Button CancelButton;
        
        [field: Header("Item Slot")]
        [field: SerializeField] private GameObject ItemSlotPrefab;
        [field: SerializeField] private Transform SpawnParent;
        private readonly List<ItemSlot> ItemSlots = new();

        private void OnEnable()
        {
            CancelButton.OnClick += OnCancelButtonClicked;
        }
        
        private void OnDisable()
        {
            CancelButton.OnClick -= OnCancelButtonClicked;
        }

        public void Show(DishSO selectedDishData, Action onConfirm)
        {
            selectedDishData.GetRecipeSheet(out var recipeSheet);

            foreach (var item in recipeSheet)
            {
                var slot = Instantiate(ItemSlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<ItemSlot>();
                ItemSlots.Add(slotScript);
                slotScript.Add(item);
            }
            
            UIObject.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(UICanvasGroup, new DoFade_CanvasGroup(1, .2f, Ease.Linear),
                onComplete: () =>
                {
                    ConfirmButton.OnClick += OnConfirmButtonClicked;
                    ConfirmButton.SetInteractable(true);
                    CancelButton.SetInteractable(true);
                });
            return;

            void OnConfirmButtonClicked()
            {
                if (ItemSlots.Any(itemSlot => itemSlot.IsEmpty()))
                {
                    MessageSystem.ShowTipUI(new PopUpUIContent(
                        message: "必須放置所有食材",
                        confirmButtonTitle: "確認",
                        cancelButtonTitle: string.Empty,
                        closeButtonTitle: string.Empty));
                    return;
                }

                onConfirm?.Invoke();
            }
        }

        public void Hide(Action onComplete = null)
        {
            ConfirmButton.SetInteractable(false);
            CancelButton.SetInteractable(false);
            DoAnimation.DoFade_CanvasGroup(UICanvasGroup, new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                onComplete: () =>
                {
                    UIObject.SetActive(false);
                    foreach (var itemSlot in ItemSlots)
                        Destroy(itemSlot.gameObject);
                    ItemSlots.Clear();
                    onComplete?.Invoke();
                });
        }
        
        private void OnCancelButtonClicked()
        {
            Hide();
        }
    }
}