using System.Collections;
using System.Cook.Cook_Selection.System.Child;
using System.Cook.Cookware;
using System.Message.Main;
using Data.General;
using Data.General.Enum;
using UnityEngine;

namespace System.Cook.Cook_Selection.System.Main
{
    internal sealed class CookSelectionSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private SelectionSystem SelectionSystem;
        [field: SerializeField] private PutIngredientSystem PutIngredientSystem;
        
        [field: Header("Other System")]
        [field: SerializeField] private MessageSystem MessageSystem;

        private IEnumerator CloseUICor;

        private void OnEnable()
        {
            CookwareSystem.OnClickEmptyBubble += OnEmptyBubbleClicked;
        }
        
        private void OnDisable()
        {
            CookwareSystem.OnClickEmptyBubble -= OnEmptyBubbleClicked;
            if (CloseUICor is not null)
            {
                StopCoroutine(CloseUICor);
                CloseUICor = null;
            }
        }

        private void OnEmptyBubbleClicked(CookType cookwareType, Action<CookDish> onConfirm, Action onCancel)
        {
            SelectionSystem.Show(cookwareType,
                onSelect: selectedDishData =>
                {
                    SelectionSystem.SetInteractable(false);
                    PutIngredientSystem.Show(selectedDishData,
                        onConfirm: () =>
                        {
                            PutIngredientSystem.SetInteractable(false);
                            if (PutIngredientSystem.CheckRecipeIsCorrect())
                            {
                                CloseUICor = CloseUICoroutine();
                                StartCoroutine(CloseUICor);
                            }
                            else
                            {
                                MessageSystem.ShowTipUI(
                                    content: new PopUpUIContent(
                                        message: "必須放置所有食材",
                                        confirmButtonTitle: "確認",
                                        cancelButtonTitle: string.Empty,
                                        closeButtonTitle: string.Empty),
                                    onConfirm: () => PutIngredientSystem.SetInteractable(true));
                            }

                            return;

                            IEnumerator CloseUICoroutine()
                            {
                                var totalCookTime = 0f;
                                var totalPrice = 0;
                
                                // Dish
                                selectedDishData.GetCookTime(out var dishCookTime);
                                selectedDishData.GetPrice(out var dishPrice);
                                totalCookTime += dishCookTime;
                                totalPrice += dishPrice;
                
                                // Ingredient
                                PutIngredientSystem.GetIngredients(out var ingredients);
                                foreach (var ingredient in ingredients)
                                {
                                    ingredient.GetCookTime(out var ingredientCookTime);
                                    ingredient.GetPrice(out var ingredientPrice);
                                    totalCookTime += ingredientCookTime;
                                    totalPrice += ingredientPrice;
                                }

                                var cookDish = new CookDish(selectedDishData, totalCookTime, totalPrice);
                                
                                // UI
                                var isSelectionUIClosed = false;
                                var isPutIngredientUIClosed = false;
                                SelectionSystem.Hide(
                                    onComplete: () => isSelectionUIClosed = true);
                                PutIngredientSystem.Hide(
                                    onComplete: () => isPutIngredientUIClosed = true);
                                yield return new WaitUntil(() => isSelectionUIClosed && isPutIngredientUIClosed);
                                onConfirm?.Invoke(cookDish);
                            }
                        },
                        onCancel: () =>
                        {
                            PutIngredientSystem.Hide(
                                onComplete: () =>
                                {
                                    SelectionSystem.SetInteractable(true);
                                });
                        });
                },
                onClose: () =>
                {
                    SelectionSystem.Hide(
                        onComplete: () =>
                        {
                            // TODO
                        });
                });
        }
    }
}