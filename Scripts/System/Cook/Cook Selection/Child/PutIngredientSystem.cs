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
        [field: SerializeField] private Button CookButton;
        [field: SerializeField] private Button ReturnButton;
        
        [field: Header("Item Slot")]
        [field: SerializeField] private GameObject ItemSlotPrefab;
        [field: SerializeField] private Transform SpawnParent;
        private readonly List<ItemSlot> ItemSlots = new();
        
        private DishSO SelectedDishData;

        public void Show(DishSO selectedDish, Action onCook, Action onReturn)
        {
            SelectedDishData = selectedDish;
            SelectedDishData.GetRecipeSheet(out var recipeSheet);

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
                    CookButton.OnClick += OnClickCookButton;
                    CookButton.SetInteractable(true);
                    ReturnButton.OnClick += OnClickReturnButton;
                    ReturnButton.SetInteractable(true);
                });
            return;

            void OnClickCookButton()
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

                TurnOffAllButtons();
                onCook?.Invoke();
            }
            
            void OnClickReturnButton()
            {
                TurnOffAllButtons();
                DoAnimation.DoFade_CanvasGroup(UICanvasGroup, new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        UIObject.SetActive(false);
                        foreach (var itemSlot in ItemSlots)
                            Destroy(itemSlot.gameObject);
                        ItemSlots.Clear();
                        onReturn?.Invoke();
                    });
            }

            void TurnOffAllButtons()
            {
                CookButton.SetInteractable(false);
                CookButton.OnClick -= OnClickCookButton;
                ReturnButton.SetInteractable(false);
                ReturnButton.OnClick -= OnClickReturnButton;
            }
        }
    }
}