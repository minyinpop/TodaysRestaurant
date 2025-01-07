using UnityEngine;

namespace Item
{
    public interface ITem
    {
        // 編號
        public int ID { get; }
        // 圖片
        public Sprite Sprite { get; }
        // 可否堆疊
        public bool Stackable { get; }
        // 最大堆疊數
        public int MaxStack { get; }
    }
}
