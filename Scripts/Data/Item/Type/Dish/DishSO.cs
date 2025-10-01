using Data.Item.Base;
using UnityEngine;

namespace Data.Item.Type.Dish
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Dish Data", fileName = "New Data")]
    internal sealed class DishSO : ScriptableObject, ITem
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