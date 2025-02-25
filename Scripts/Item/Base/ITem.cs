using UnityEngine;

namespace Item.Base
{
    /// <summary>
    /// 物品的接口，用來定義甚麼是遊戲中的物品。
    /// </summary>
    public interface ITem
    {
        // 物品編號。
        int ID { get; }
        
        // 物品圖片
        Sprite Sprite { get; }
        
        // 堆疊許可。
        bool Stackable { get; }
        
        // 最大堆疊量。 ( 就算是不可堆疊，也要設定成 1 )
        int MaxStack { get; }
    }
}
