using Bubble.Kitchenware;
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
        
        [Tooltip("用於顯示廚俱正在烹飪的氣泡的預製件。"), SerializeField]
        private GameObject cookBubblePrefab;
        
        [Tooltip("用於顯示廚俱已經做完料理的氣泡的預製件。"), SerializeField]
        private GameObject doneBubblePrefab;
        
        [Tooltip("- 用於顯示該廚俱裡烹飪結售後，\n- 所產生的料理的預製件。"), SerializeField]
        private GameObject cuisineBubblePrefab;
        
        
        
        [Header("資料庫"), Tooltip("用來顯示氣泡的圖片的資料庫。"), SerializeField]
        private KitchenwareBubbleData kitchenwareBubbleData;
        
        
        
        // 當前的氣泡的遊戲物件的暫存。
        public GameObject Bubble { get; private set; }
        
        // 自身的 KitchenwareManager 組件，用來傳入氣泡裡的 KitchenwareManager 做使用。
        private KitchenwareManager _kitchenwareManager;

        private void Awake()
        {
            _kitchenwareManager = GetComponent<KitchenwareManager>();
        }
        
        private void Start()
        {
            InitEmptyBubble();
        }
        
        
        
        /// <summary>
        /// 用來生成空廚具的氣泡的方法。
        /// </summary>
        public void InitEmptyBubble()
        {
            Bubble = Instantiate(emptyBubblePrefab, bubbleSpawnPoint);
            Bubble.GetComponent<KitchenwareBubbleBase>().InitBubble(_kitchenwareManager, kitchenwareBubbleData);
        }

        /// <summary>
        /// 用來生成小遊戲的氣泡的方法。
        /// </summary>
        public void InitCookBubble()
        {
            DeleteBubbleIfExist();
            
            Bubble = Instantiate(cookBubblePrefab, bubbleSpawnPoint);
            Bubble.GetComponent<KitchenwareBubbleBase>().InitBubble(_kitchenwareManager, kitchenwareBubbleData);
        }

        /// <summary>
        /// 用來生成烹飪結束的氣泡的方法。
        /// </summary>
        public void InitDoneBubble()
        {
            DeleteBubbleIfExist();
            
            Bubble = Instantiate(doneBubblePrefab, bubbleSpawnPoint);
            Bubble.GetComponent<KitchenwareBubbleBase>().InitBubble(_kitchenwareManager, kitchenwareBubbleData);
        }

        public void InitCuisineBubble()
        {
            DeleteBubbleIfExist();
            
            Bubble = Instantiate(cuisineBubblePrefab, bubbleSpawnPoint);
            Bubble.GetComponent<KitchenwareBubbleBase>().InitBubble(_kitchenwareManager, kitchenwareBubbleData);
        }

        /// <summary>
        /// 用來檢測氣泡是否已經存在的方法，並刪除舊的氣泡。
        /// </summary>
        private void DeleteBubbleIfExist()
        {
            if (Bubble is not null)
            {
                Destroy(Bubble);
                Bubble = null;
            }
        }
        
        /// <summary>
        /// 用來改變當前氣泡的互動模式的方法。
        /// </summary>
        /// <param name="interactable"> 新傳入的互動。 </param>
        public void ChangeButtonInteractable(bool interactable)
        {
            if (Bubble is null)
                return;
            
            Bubble.GetComponent<Image>().raycastTarget = interactable;
        }
    }
}
