using Item.Interface;
using UnityEngine;

namespace Storage.Root.Backend.Struct
{
    [System.Serializable]
    public struct StorageSlotData
    {
        [Tooltip("該儲物格是否被上鎖 ?")]
        public bool Locked;
        
        [Tooltip("該儲物格的物品。")]
        public ITem Item;
        
        [Tooltip("該儲物格的物品數量。")]
        public int Quantity;
    }
}