using DataBase.Bubble.Kitchenware;
using Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Bubble.Kitchenware.Category
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class KitchenwareCookBubble : KitchenwareBubbleBase
    {
        [Header("氣泡設定"), Tooltip("用來顯示氣泡圖案的圖片組件。"), SerializeField]
        private Image gameIconImage;
        
        [Tooltip("用來顯示該玩小遊戲的圖片組件。"), SerializeField]
        private Image gameTimeIconImage;
        
        // 自身的 Button 組件，用於開關玩家是否可以互動。
        private Button _button;

        // 用來判斷現在是否可以遊玩小遊戲。
        private bool _timeToPlay;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        /// <summary>
        /// 用來初始化氣泡的方法。
        /// </summary>
        public override void InitBubble(KitchenwareManager kitchenwareManager, KitchenwareBubbleData newData)
        {
            KitchenwareManager = kitchenwareManager;
            KitchenwareBubbleData = newData;
            
            gameIconImage.sprite = KitchenwareBubbleData.GameBubbleSprite;
        }

        /// <summary>
        /// 用來提示玩家該玩小遊戲的方法。
        /// </summary>
        public override void IsGameCanPlay(bool canPlay)
        {
            _button.interactable = canPlay;
            gameTimeIconImage.gameObject.SetActive(canPlay);
        }
    }
}
