using System;
using System.Collections;
using Common.Item.Data.Food.Custom_Food;
using Common.Value;
using Common.Value.Type;
using Input_System;
using Restaurant_System.Object.Cookware.Object.Cook_Selection.System.Child;
using Restaurant_System.Object.Cookware.System;
using UI_System.Message_UI_System.Main;
using UnityEngine;

namespace Restaurant_System.Object.Cookware.Object.Cook_Selection.System.Main
{
    public sealed class CookSelectionSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private SelectionSystem selectionSystem;
        [field: SerializeField] private PutIngredientSystem putIngredientSystem;
        
        private IEnumerator _closeUICor;
        
        public static event Action OnSelectionUIOpen;
        public static event Action OnPutIngredientUIOpen;
        public static event Action OnPutIngredientUIClose;
        
        private void Awake()
        {
            CookwareSystem.OpenCookSelectionUI += Open;
            CookwareSystem.CloseCookSelectionUI += Close;
        }
        
        private void OnDisable()
        {
            if (_closeUICor is not null)
            {
                StopCoroutine(_closeUICor);
                _closeUICor = null;
            }
        }

        private void OnDestroy()
        {
            CookwareSystem.OpenCookSelectionUI -= Open;
            CookwareSystem.CloseCookSelectionUI -= Close;
        }

        private void Open(CookType cookwareType, Action<CustomFoodItem> onConfirm, Action onCancel)
        {
            OnSelectionUIOpen?.Invoke();
            
            selectionSystem.Show(cookwareType,
                onSelect: selectedDishData =>
                {
                    OnPutIngredientUIOpen?.Invoke();
                    
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
                                MessageUISystem.ShowTipUI(
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

                                #region Dish
                                    totalCookTime += selectedDishData.CookTime;
                                    totalPrice += selectedDishData.Price;
                                #endregion
                
                                #region Ingredient
                                    putIngredientSystem.GetIngredients(out var ingredients);
                                    foreach (var ingredient in ingredients)
                                    {
                                        totalCookTime += ingredient.CookTime;
                                        totalPrice += ingredient.Price;
                                    }

                                    var cookDish = new CustomFoodItem();
                                    cookDish.Initialize(selectedDishData, totalCookTime, totalPrice);
                                #endregion
                                
                                #region UI
                                    var isSelectionUIClosed = false;
                                    var isPutIngredientUIClosed = false;
                                    selectionSystem.Hide(
                                        onComplete: () =>
                                        {
                                            isSelectionUIClosed = true;
                                        });
                                    putIngredientSystem.Hide(
                                        onComplete: () =>
                                        {
                                            isPutIngredientUIClosed = true;
                                            
                                            OnPutIngredientUIClose?.Invoke();
                                        });
                                    yield return new WaitUntil(() => isSelectionUIClosed && isPutIngredientUIClosed);
                                    onConfirm?.Invoke(cookDish);
                                #endregion
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