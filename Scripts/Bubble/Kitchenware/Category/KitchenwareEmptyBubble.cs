using DataBase.Bubble.Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Bubble.Kitchenware.Category
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class KitchenwareEmptyBubble : KitchenwareBubble
    {
        [Header("氣泡設定"), Tooltip("用來顯示氣泡圖案的圖片組件。"), SerializeField]
        private Image emptyIconImage;
        
        // 用來顯示氣泡的圖片的資料庫。
        private KitchenwareBubbleData _kitchenwareBubbleData;

        /// <summary>
        /// 用來初始化氣泡的方法。
        /// </summary>
        public override void InitBubble(KitchenwareBubbleData newData)
        {
            _kitchenwareBubbleData = newData;
            emptyIconImage.sprite = _kitchenwareBubbleData.EmptyBubbleSprite;
        }
        
        /// <summary>
        /// 用於 Button 組件的 OnClick() 做使用。
        /// </summary>
        public void OnClick()
        {
            print("Clicked !");
        }
    }
}
