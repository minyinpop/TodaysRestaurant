using DataBase.Bubble.Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchenware
{
    /// <summary>
    /// 用來管理廚俱的氣泡的類。
    /// </summary>
    [RequireComponent(typeof(KitchenwareManager))]
    public class KitchenwareBubbleSystem : MonoBehaviour
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
        
        // 自身的 KitchenwareManager 組件，用來傳入氣泡裡的 KitchenwareManager 做使用。
        private KitchenwareManager _kitchenwareManager;

        private void Awake()
        {
            _kitchenwareManager = GetComponent<KitchenwareManager>();
        }
        
        private void Start()
        {
            _bubble = Instantiate(emptyBubblePrefab, bubbleSpawnPoint);
            _bubble.GetComponent<Bubble.Kitchenware.KitchenwareBubbleBase>().InitBubble(_kitchenwareManager, kitchenwareBubbleData);
        }

        /// <summary>
        /// 用來改變當前氣泡的互動模式的方法。
        /// </summary>
        /// <param name="interactable"> 新傳入的互動。 </param>
        public void ChangeButtonInteractable(bool interactable)
        {
            if (_bubble is null)
                return;
            
            _bubble.GetComponent<Button>().interactable = interactable;
        }
    }
}
