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

        private void OnEnable()
        {
            CookwareSystem.OnClickEmptyBubble += OnClickEmptyBubble;
        }

        private void OnDisable()
        {
            CookwareSystem.OnClickEmptyBubble -= OnClickEmptyBubble;
        }

        private void OnClickEmptyBubble(Action<DishSO> onConfirm)
        {
            SelectionSystem.Show(
                onSelect: selectedDishData =>
                {
                    PutIngredientSystem.Show(selectedDishData,
                        onCook: () =>
                        {
                            Debug.Log("Cooked.");
                        },
                        onReturn: () =>
                        {
                            // TODO Return.
                        });
                });
        }
    }
}