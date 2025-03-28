using DataBase.Bubble.Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Bubble.Kitchenware.Category
{
    public class KitchenwareDoneBubble : KitchenwareBubble
    {
        [Header("氣泡設定"), Tooltip("用來顯示氣泡圖案的圖片組件。"), SerializeField]
        private Image doneIconImage;
        
        // 用來顯示氣泡的圖片的資料庫。
        private KitchenwareBubbleData _kitchenwareBubbleData;

        /// <summary>
        /// 用來初始化氣泡的方法。
        /// </summary>
        public override void InitBubble(KitchenwareBubbleData newData)
        {
            _kitchenwareBubbleData = newData;
            doneIconImage.sprite = _kitchenwareBubbleData.EmptyBubbleSprite;
        }
    }
}
