using UnityEngine;

namespace DataBase.Bubble.Kitchenware
{
    /// <summary>
    /// 用來設定廚俱的氣泡的圖片的資料庫。
    /// </summary>
    [CreateAssetMenu(fileName = "New Kitchenware Bubble Data", menuName = "Kitchenware Bubble Data", order = 2)]
    public class KitchenwareBubbleData : ScriptableObject
    {
        [field: Header("圖片"), Tooltip("用來顯示廚俱空閒的氣泡的圖片。"), SerializeField]
        public Sprite EmptyBubbleSprite { get; private set; }
        
        [field: Tooltip("用來顯示廚俱可以玩小遊戲時的氣泡的圖片。"), SerializeField]
        public Sprite GameBubbleSprite { get; private set; }
        
        [field: Tooltip("用來顯示廚俱完成料理的氣泡的圖片。"), SerializeField]
        public Sprite DoneBubbleSprite { get; private set; }
    }
}
