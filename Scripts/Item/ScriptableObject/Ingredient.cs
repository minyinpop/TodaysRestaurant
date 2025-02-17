using Item.Interface;
using UnityEngine;

namespace Item.ScriptableObject
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient", fileName = "New Ingredient", order = 1)]
    public class Ingredient : UnityEngine.ScriptableObject, ITem
    {
        [Header("訊息設定")]
        [Tooltip("物品的編號。")]
        private int _id;
        public int ID => _id;
        
        [Tooltip("物品的圖片。")]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [Header("堆疊設定")]
        [Tooltip("是否可以堆疊 ?")]
        private bool _stackable;
        public bool Stackable => _stackable;

        [Tooltip("最大堆疊數量。")]
        private int _maxStack;
        public int MaxStack => _maxStack;
    }
}