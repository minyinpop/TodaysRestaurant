using UnityEngine;

namespace Item.Core
{
    public class ItemCore : ScriptableObject
    {
        [field: Tooltip("編號"), SerializeField]
        public int ID { get; private set; }
        
        [field: Tooltip("圖片"), SerializeField]
        public Sprite Sprite { get; private set; }

        [field: Tooltip("是否可以堆疊 ?"), SerializeField]
        public StackType Stack { get; private set; }
        public enum StackType
        {
            Yes,
            No
        }
        
        [field: Tooltip("最大堆疊數"), SerializeField]
        public int MaxStack { get; private set; }
    }
}
