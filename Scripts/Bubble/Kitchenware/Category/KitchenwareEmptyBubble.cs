using DataBase.Bubble.Kitchenware;
using Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Bubble.Kitchenware.Category
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class KitchenwareEmptyBubble : KitchenwareBubbleBase
    {
        [Header("氣泡設定"), Tooltip("用來顯示氣泡圖案的圖片組件。"), SerializeField]
        private Image emptyIconImage;

        /// <summary>
        /// 用來初始化氣泡的方法。
        /// </summary>
        public override void InitBubble(KitchenwareManager kitchenwareManager ,KitchenwareBubbleData newData)
        {
            KitchenwareManager = kitchenwareManager;
            KitchenwareBubbleData = newData;
            
            emptyIconImage.sprite = KitchenwareBubbleData.EmptyBubbleSprite;
        }
    }
}
