using System.Collections;
using DataBase.Item.Category.Cuisine;
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

        
        
        // 用來區分當前廚俱的狀態的參數。
        public KitchenwareStateEnum KitchenwareState { get; private set; } = KitchenwareStateEnum.Empty;
        public enum KitchenwareStateEnum
        {
            Empty,
            Game,
            Done
        }
        
        
        
        [Header("預製件"), Tooltip("- 料理選擇介面的預製件。\n- 依照 PlayerChooseCuisineData 來顯示。"), SerializeField]
        private GameObject cuisineChooseUIPrefab;
        
        [Tooltip("料理選擇介面要在哪裡生成。"), SerializeField]
        private Transform cuisineChooseUISpawnPoint;
        
        // 料理選擇介面的暫存。
        private GameObject _cuisineChooseUI;
        
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
            _kitchenwareBubbleSystem.ChangeButtonInteractable(isNearby);

            // 如果玩家在開啟料理選擇介面的時候遠離廚俱，就刪除介面，並且清空暫存資料。
            if (!isNearby && _cuisineChooseUI is not null)
            {
                Destroy(_cuisineChooseUI);
                _cuisineChooseUI = null;
            }
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
                    break;
                }
                case KitchenwareStateEnum.Done:
                {
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
            
            _kitchenwareCookSystem.StartCookProcess(newCuisineData);
        }
    }
}
