using UnityEngine;
using UnityEngine.UI;

namespace NPC.Customer.Bubble.Category
{
    /// <summary>
    /// 用於 NPC 點餐時，頭上所顯示的氣泡狀態，會告訴玩家當前 NPC 的狀態是甚麼。
    /// 繼承了 ThinkBubble 這個抽象類。
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class OrderThinkBubble : ThinkBubble
    {
        [Header("圖片顯示組件"), Tooltip("用來顯示點餐前思考的圖片組件，用於 UI。"), SerializeField]
        private Image thinkingImage;
        
        [Tooltip("用來顯示要點餐時的圖片組件，用於 UI。"), SerializeField]
        private Image orderImage;

        [Tooltip("用來顯示要甚麼餐點的圖片組件，用於 UI。"), SerializeField]
        private Image cuisineImage;
        
        [Tooltip("用來顯示當前 NPC 還剩下多少耐心的遮罩的圖片組件，用於 UI。"), SerializeField]
        private Image maskImage;
        
        // 自身的 Button 組件，用來偵測玩家是否點擊氣泡
        private Button _button;

        // 當前角色的點餐狀況
        private ThinkBubbleState _state;
        private enum ThinkBubbleState
        {
            Thinking,
            Ordering,
            WaitingForCuisine
        };
    }
}
