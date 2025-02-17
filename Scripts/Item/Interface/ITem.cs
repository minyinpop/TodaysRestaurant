using UnityEngine;

namespace Item.Interface
{
    public interface ITem
    {
        // 物品的編號。
        int ID { get; }

        // 物品的圖片。
        Sprite Sprite { get; }

        // 是否可以堆疊 ?
        bool Stackable { get; }

        // 最大堆疊數量。
        int MaxStack { get; }
    }
}