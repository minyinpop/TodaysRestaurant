using System;
using System.Collections;
using System.Collections.Generic;
using Common.Item.Custom;
using Common.Value;
using Common.Value.Type;
using Restaurant_System.Object.Cookware.Object.Cook_Selection.System.Child;
using Restaurant_System.Object.Cookware.System;
using UI_System.Main;
using UnityEngine;

namespace Restaurant_System.Object.Cookware.Object.Cook_Selection.System.Main
{
    public sealed class CookSelectionSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private SelectionSystem selectionSystem;
        [field: SerializeField] private PutIngredientSystem putIngredientSystem;

        private IEnumerator _closeUICor;

        private readonly Queue<Action> _cleansUpActions = new();

        private void OnEnable()
        {
            CookwareSystem.OpenCookSelectionUI += Open;
            _cleansUpActions.Enqueue(() => CookwareSystem.OpenCookSelectionUI -= Open);
            
            CookwareSystem.CloseCookSelectionUI += Close;
            _cleansUpActions.Enqueue(() => CookwareSystem.CloseCookSelectionUI -= Close);
        }
        
        private void OnDisable()
        {
            while (_cleansUpActions.Count > 0) _cleansUpActions.Dequeue()?.Invoke();
            
            if (_closeUICor is not null)
            {
                StopCoroutine(_closeUICor);
                _closeUICor = null;
            }
        }

        private void Open(CookType cookwareType, Action<CustomItem> onConfirm, Action onCancel)
        {
            selectionSystem.Show(cookwareType,
                onSelect: selectedDishData =>
                {
                    selectionSystem.SetInteractable(false);
                    putIngredientSystem.Show(selectedDishData,
                        onConfirm: () =>
                        {
                            putIngredientSystem.SetInteractable(false);
                            if (putIngredientSystem.CheckRecipeIsCorrect())
                            {
                                _closeUICor = CloseUICoroutine();
                                StartCoroutine(_closeUICor);
                            }
                            else
                            {
                                UISystem.ShowTipUI(
                                    content: new PopUpUIContent(
                                        message: "必須放置所有食材",
                                        confirmButtonTitle: "確認",
                                        cancelButtonTitle: string.Empty,
                                        closeButtonTitle: string.Empty),
                                    onConfirm: () => putIngredientSystem.SetInteractable(true));
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
                                putIngredientSystem.GetIngredients(out var ingredients);
                                foreach (var ingredient in ingredients)
                                {
                                    ingredient.GetCookTime(out var ingredientCookTime);
                                    ingredient.GetPrice(out var ingredientPrice);
                                    totalCookTime += ingredientCookTime;
                                    totalPrice += ingredientPrice;
                                }

                                var cookDish = ScriptableObject.CreateInstance<CustomItem>();
                                cookDish.Initialize(selectedDishData, totalCookTime, totalPrice);
                                
                                // UI
                                var isSelectionUIClosed = false;
                                var isPutIngredientUIClosed = false;
                                selectionSystem.Hide(
                                    onComplete: () => isSelectionUIClosed = true);
                                putIngredientSystem.Hide(
                                    onComplete: () => isPutIngredientUIClosed = true);
                                yield return new WaitUntil(() => isSelectionUIClosed && isPutIngredientUIClosed);
                                onConfirm?.Invoke(cookDish);
                            }
                        },
                        onCancel: () =>
                        {
                            putIngredientSystem.Hide(
                                onComplete: () =>
                                {
                                    selectionSystem.SetInteractable(true);
                                });
                        });
                },
                onClose: () =>
                {
                    selectionSystem.Hide(
                        onComplete: () =>
                        {
                            // TODO
                        });
                });
        }

        public void Close()
        {
            putIngredientSystem.Hide();
            selectionSystem.Hide();
        }
    }
}