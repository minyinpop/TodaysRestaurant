using Common.Item_Slot.Main;
using Common.Item.Data.Ingredient;

namespace Common.Item_Slot.Child
{
    public sealed class PutIngredientSlot : StorageSlot
    {
        private IIngredient _targetIngredient;
        private IIngredient _currentIngredient;
        
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!interactable) return;
                animation.DoScale_UI(slotRect, scaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!interactable) return;
                animation.DoScale_UI(slotRect, scaleDownSettings);
            }
        #endregion

        #region Item
            public override bool TryAddItem(IIngredient ingredient)
            {
                if (ingredient is null) return false;
                if (_currentIngredient is not null) return false;
                
                if (_targetIngredient is null)
                {
                    _targetIngredient = ingredient;
                    itemImage.sprite = ingredient.ItemSprite;
                }
                else
                {
                    // TODO 物品類型相同但等級比 TargetItemData 還低，一樣跳出 Message System
                    if (ingredient.IngredientTier < _targetIngredient.IngredientTier)
                    {
                        return false;
                    }

                    _currentIngredient = ingredient;
                    itemImage.sprite = ingredient.ItemSprite;
                    itemImage.color = haveItemColor;
                }

                return true;
            }

            public void TryGetItem(out IIngredient itemData)
            {
                if (_currentIngredient == null)
                {
                    itemData = null;
                    return;
                }

                itemData = _currentIngredient;
                _currentIngredient = null;
                itemImage.sprite = _targetIngredient.ItemSprite;
                itemImage.color = noItemColor;
            }
        #endregion
    }
}