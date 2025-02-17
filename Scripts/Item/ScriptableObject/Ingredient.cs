using Item.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item.ScriptableObject
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient", fileName = "New Ingredient", order = 1)]
    public class Ingredient : UnityEngine.ScriptableObject, ITem
    {
        [Header("訊息設定")]
        [Tooltip("物品的編號。")]
        [SerializeField]
        private int id;
        public int ID => id;
        
        [Tooltip("物品的圖片。")]
        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;

        [Header("堆疊設定")]
        [Tooltip("是否可以堆疊 ?")]
        [SerializeField]
        private bool stackable;
        public bool Stackable => stackable;

        [Tooltip("最大堆疊數量。")]
        [SerializeField]
        private int maxStack;
        public int MaxStack => maxStack;
    }
}