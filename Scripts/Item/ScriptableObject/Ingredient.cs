using Item.Interface;
using UnityEngine;

namespace Item.ScriptableObject
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient", fileName = "New Ingredient", order = 1)]
    public class Ingredient : UnityEngine.ScriptableObject, ITem
    {
        [Header("訊息設定"), Tooltip("物品的編號。")]
        public int ID;
        [Tooltip("物品的圖片。")]
        public Sprite Sprite;
        
        [Header("堆疊設定"), Tooltip("是否可以堆疊 ?")]
        public bool Stackable;
        [Tooltip("最大堆疊數量。")]
        public int MaxStack;
    }
}