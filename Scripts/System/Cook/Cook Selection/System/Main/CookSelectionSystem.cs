using System.Collections;
using System.Cook.Cook_Selection.System.Child;
using System.Cook.Cookware;
using System.Message.Main;
using Data.General;
using Data.Item.Type.Dish;
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

        private void OnEmptyBubbleClicked(Action<DishSO> onConfirm, Action onCancel)
        {
            SelectionSystem.Show(
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
                                var isSelectionUIClosed = false;
                                var isPutIngredientUIClosed = false;
                                SelectionSystem.Hide(
                                    onComplete: () => isSelectionUIClosed = true);
                                PutIngredientSystem.Hide(
                                    onComplete: () => isPutIngredientUIClosed = true);
                                yield return new WaitUntil(() => isSelectionUIClosed && isPutIngredientUIClosed);
                                onConfirm?.Invoke(selectedDishData);
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
                            Debug.Log("Can Click Empty Bubble.");
                        });
                });
        }
    }
}