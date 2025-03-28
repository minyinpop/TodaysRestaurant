using DataBase.Bubble.Kitchenware;
using UnityEngine;

namespace Bubble.Kitchenware
{
    /// <summary>
    /// 用來定義廚俱的氣泡的抽象類。
    /// </summary>
    public abstract class KitchenwareBubble : MonoBehaviour
    {
        /// <summary>
        /// 用來初始化氣泡的方法。
        /// </summary>
        public abstract void InitBubble(KitchenwareBubbleData newData);
    }
}
