using UnityEngine;

namespace Item
{
    public abstract class ItemCore : ScriptableObject
    {
        [field: Header("核心資訊"), Tooltip("物品編號"), SerializeField]
        public int ID { get; private set; }
        
        [field: Tooltip("物品圖片"), SerializeField]
        public Sprite Sprite { get; private set; }
        
        [field: Tooltip("是否可以堆疊?"), SerializeField]
        public bool Stackable { get; private set; }
        
        [field: Tooltip("最大堆疊數量"), Range(max: 99, min: 1), SerializeField]
        public int MaxStack { get; private set; }
        
        [field: Tooltip("烹飪時間"), SerializeField]
        public int CookingTIme { get; private set; }
        
        [field: Tooltip("價格"), SerializeField]
        public int Price { get; private set; }
    }
}
