using Bubble.Kitchenware;
using DataBase.Item.Category.Cuisine;
using Game.Stockpot;
using Kitchenware.Cuisine_Choose_UI;
using UnityEngine;

namespace Kitchenware
{
    /// <summary>
    /// 用來管理廚具的類。
    /// 需要 KitchenwareDetector 與 KitchenwareBubble 這兩個類的支援。
    /// </summary>
    [RequireComponent(typeof(KitchenwareCookSystem))]
    [RequireComponent(typeof(KitchenwareDetectorSystem))]
    [RequireComponent(typeof(KitchenwareBubbleSystem))]
    public class KitchenwareManager : MonoBehaviour
    {
        // 自身的 KitchenwareCookSystem 組件，用來執行廚俱烹飪的類。
        private KitchenwareCookSystem _kitchenwareCookSystem;
        
        // 自身的 KitchenwareDetectorSystem 組件，用來檢測玩家是否進入偵測空間的類。
        private KitchenwareDetectorSystem _kitchenwareDetectorSystem;
        
        // 自身的 KitchenwareBubbleSystem 組件，用來管理廚俱的氣泡的類。
        private KitchenwareBubbleSystem _kitchenwareBubbleSystem;

        // 當前廚俱所烹飪的料理的資料。
        public Cuisine CuisineData { get; set; }
        
        
        
        // 用來區分當前廚俱的狀態的參數。
        private KitchenwareStateEnum KitchenwareState { get; set; } = KitchenwareStateEnum.Empty;
        private enum KitchenwareStateEnum
        {
            Empty,
            Game,
            Done,
            CuisineAppear
        }
        
        
        
        [Header("料理選擇介面"), Tooltip("- 料理選擇介面的預製件。\n- 依照 PlayerChooseCuisineData 來顯示。"), SerializeField]
        private GameObject cuisineChooseUIPrefab;
        
        [Tooltip("料理選擇介面要在哪裡生成。"), SerializeField]
        private Transform cuisineChooseUISpawnPoint;
        
        // 料理選擇介面的暫存。
        private GameObject _cuisineChooseUI;
        
        
        
        [Header("小遊戲"), Tooltip("該廚俱的小遊戲的預製件。"), SerializeField]
        private GameObject gamePrefab;
        
        [Tooltip("小遊戲的生成位置。"), SerializeField]
        private Transform gameSpawnPoint;
        
        // 小遊戲的遊戲物件的暫存。
        private GameObject _game;



        // 當玩家獲取廚具中的料理時，就會觸發這個廣播。
        public static event System.Func<Cuisine, bool> cuisineDeliver;
        
        private void Awake()
        {
            _kitchenwareCookSystem = GetComponent<KitchenwareCookSystem>();
            _kitchenwareDetectorSystem = GetComponent<KitchenwareDetectorSystem>();
            _kitchenwareBubbleSystem = GetComponent<KitchenwareBubbleSystem>();
        }

        /// <summary>
        /// 用來執行玩家是否在附近的方法。
        /// </summary>
        /// <param name="isNearby"> 玩家是否在附近。 </param>
        public void OnPlayerNearby(bool isNearby)
        {
            // 如果玩家打開料理選擇介面，就讓現有的氣泡無法互動。
            if (_cuisineChooseUI is not null)
            {
                _kitchenwareBubbleSystem.ChangeButtonInteractable(false);

                // 如果玩家遠離廚俱的附近，就關閉料理選擇介面，並清空 _cuisineChooseUI 的暫存。
                if (!isNearby)
                {
                    Destroy(_cuisineChooseUI);
                    _cuisineChooseUI = null;
                }
                
                return;
            }
            
            _kitchenwareBubbleSystem.ChangeButtonInteractable(isNearby);
        }

        /// <summary>
        /// 用來呼叫 KitchenwareBubbleSystem 組件裡面，當氣泡被按下時，所發生的方法。
        /// </summary>
        public void OnBubbleClick()
        {
            switch (KitchenwareState)
            {
                case KitchenwareStateEnum.Empty:
                {
                    _cuisineChooseUI = Instantiate(cuisineChooseUIPrefab, cuisineChooseUISpawnPoint);
                    _cuisineChooseUI.GetComponent<KitchenwareCuisineChooseUI>().InitUI(this);
                    break;
                }
                case KitchenwareStateEnum.Game:
                {
                    _kitchenwareBubbleSystem.Bubble.GetComponent<KitchenwareBubbleBase>().IsGameCanPlay(false);
                    
                    _game = Instantiate(gamePrefab, gameSpawnPoint);
                    _game.GetComponent<StockpotManager>().OnInit(this);
                    break;
                }
                case KitchenwareStateEnum.Done:
                {
                    _kitchenwareBubbleSystem.InitCuisineBubble();
                    KitchenwareState = KitchenwareStateEnum.CuisineAppear;
                    break;
                }
                case KitchenwareStateEnum.CuisineAppear:
                {
                    if (cuisineDeliver?.Invoke(CuisineData) == true)
                    {
                        CuisineData = null;
                        
                        _kitchenwareBubbleSystem.InitEmptyBubble();
                        KitchenwareState = KitchenwareStateEnum.Empty;
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// 用於開始烹飪的方法。
        /// </summary>
        /// <param name="newCuisineData"> 玩家所選擇的料理的資料。 </param>
        public void StartCook(Cuisine newCuisineData)
        {
            Destroy(_cuisineChooseUI);
            _cuisineChooseUI = null;
            
            KitchenwareState = KitchenwareStateEnum.Game;
            
            _kitchenwareCookSystem.StartCook(newCuisineData);
        }

        /// <summary>
        /// 執行小遊戲結束時的方法。
        /// </summary>
        public void OnGameFinish()
        {
            Destroy(_game);
            _game = null;
            
            KitchenwareState = KitchenwareStateEnum.Done;
            
            _kitchenwareCookSystem.ContinueCook();
        }
    }
}
