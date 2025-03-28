using DataBase.Bubble.Kitchenware;
using UnityEngine;

namespace Kitchenware
{
    /// <summary>
    /// 用來管理廚俱的氣泡的類。
    /// </summary>
    [RequireComponent(typeof(KitchenwareManager))]
    public class KitchenwareBubble : MonoBehaviour
    {
        [Header("生成位置"), Tooltip("氣泡的生成位置。"), SerializeField]
        private Transform bubbleSpawnPoint;
        
        
        
        [Header("預製件"), Tooltip("用於顯示廚俱是空的氣泡的預製件。"), SerializeField]
        private GameObject emptyBubblePrefab;
        
        [Tooltip("用於顯示廚俱需要遊玩小遊戲的氣泡的預製件。"), SerializeField]
        private GameObject gameBubblePrefab;
        
        [Tooltip("用於顯示廚俱已經做完料理的氣泡的預製件。"), SerializeField]
        private GameObject doneBubblePrefab;
        
        
        
        [Header("資料庫"), Tooltip("用來顯示氣泡的圖片的資料庫。"), SerializeField]
        private KitchenwareBubbleData kitchenwareBubbleData;
        
        
        
        // 當前的氣泡的遊戲物件的暫存。
        private GameObject _bubble;

        private void Start()
        {
            _bubble = Instantiate(emptyBubblePrefab, bubbleSpawnPoint);
            _bubble.GetComponent<Bubble.Kitchenware.KitchenwareBubble>().InitBubble(kitchenwareBubbleData);
        }
    }
}
