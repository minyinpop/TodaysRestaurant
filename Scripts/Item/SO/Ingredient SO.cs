using UnityEngine;

namespace Item.SO
{
    [CreateAssetMenu(menuName = "Minyinpop/New Item/New Ingredient", fileName = "New Ingredient", order = 1)]
    public class IngredientSO : ScriptableObject, ITemBase
    {
        [field: Header("基本資訊"), Tooltip("物品的編號。")]
        public int ID;
        
        [field: Tooltip("物品的圖片。")]
        public Sprite Sprite;
        
        [field: Header("堆疊設定"), Tooltip("該物品是否可以堆疊 ?")]
        public bool Stackable;
        
        [field: Tooltip("該物品的最大堆疊數量。")]
        public int MaxStack;
    }
}
