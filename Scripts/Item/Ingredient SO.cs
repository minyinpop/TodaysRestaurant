using UnityEngine;

namespace Item
{
    [CreateAssetMenu(menuName = "Item/New Ingredient", fileName = "New Data")]
    public class IngredientSO : ScriptableObject, ITem
    {
        // 編號
        public int id;
        // 圖片
        public Sprite sprite;
        // 可否堆疊
        public bool stackable;
        // 最大堆疊數
        public int maxStack;
    }
}
