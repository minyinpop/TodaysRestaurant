using Data.Item.Base;
using UnityEngine;

namespace Data.Item.Type.Ingredient
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient Data", fileName = "New Data")]
    internal sealed class IngredientSO : ScriptableObject, ITem
    {
        #region Name
        [field: Header("Name")]
        [field: SerializeField] private string ItemName;

        public void GetItemName(out string itemName)
        {
            itemName = ItemName;
        }
        #endregion
        
        #region Sprite
        [field: Header("Sprite")]
        [field: SerializeField] private Sprite ItemSprite;
        
        public void GetItemSprite(out Sprite itemSprite)
        {
            itemSprite = ItemSprite;
        }
        #endregion
    }
}