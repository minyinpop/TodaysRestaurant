using System.Collections;
using System.Cook.Cook_Selection.Child;
using System.Cook.Cookware;
using Data.Item.Type.Dish;
using UnityEngine;

namespace System.Cook.Cook_Selection.Main
{
    internal sealed class CookSelectionSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private SelectionSystem SelectionSystem;
        [field: SerializeField] private PutIngredientSystem PutIngredientSystem;

        private IEnumerator CloseUICor;

        private void OnEnable()
        {
            CookwareSystem.OnClickEmptyBubble += OnClickEmptyBubble;
        }

        private void OnDisable()
        {
            CookwareSystem.OnClickEmptyBubble -= OnClickEmptyBubble;
            if (CloseUICor is not null)
            {
                StopCoroutine(CloseUICor);
                CloseUICor = null;
            }
        }

        private void OnClickEmptyBubble(Action<DishSO> onConfirm)
        {
            SelectionSystem.Show(
                onSelect: selectedDishData =>
                {
                    PutIngredientSystem.Show(selectedDishData,
                        onConfirm: () =>
                        {
                            CloseUICor = CloseUICoroutine();
                            StartCoroutine(CloseUICor);
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
                        });
                });
        }
    }
}