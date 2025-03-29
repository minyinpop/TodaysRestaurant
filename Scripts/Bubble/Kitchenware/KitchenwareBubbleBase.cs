using DataBase.Bubble.Kitchenware;
using Kitchenware;
using UnityEngine;

namespace Bubble.Kitchenware
{
    /// <summary>
    /// 用來定義廚俱的氣泡的抽象類。
    /// </summary>
    public abstract class KitchenwareBubbleBase : MonoBehaviour
    {
        // 廚俱自身的 KitchenwareManager 組件，用來回傳點擊氣泡後，所發生的事件。
        protected KitchenwareManager KitchenwareManager;
        
        // 用來顯示氣泡的圖片的資料庫。
        protected KitchenwareBubbleData KitchenwareBubbleData;
        
        /// <summary>
        /// 用來初始化氣泡的方法。
        /// </summary>
        public abstract void InitBubble(KitchenwareManager kitchenwareManager ,KitchenwareBubbleData newData);
        
        /// <summary>
        /// 用來執行玩家是否可以遊玩小遊戲的方法。
        /// </summary>
        public virtual void IsGameCanPlay(bool canPlay) { }

        /// <summary>
        /// 用於 Button 組件的 OnClick() 做使用。
        /// </summary>
        public void OnClick()
        {
            KitchenwareManager.OnBubbleClick();
        }
    }
}
