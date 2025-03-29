using DataBase.Item.Category.Cuisine;
using DataBase.Menu.ChooseCuisine;
using UnityEngine;

namespace Kitchenware.Cuisine_Choose_UI
{
    /// <summary>
    /// 用來管理該廚俱可以製作甚麼類型的料理。
    /// </summary>
    public class KitchenwareCuisineChooseUI : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("玩家本日上架的料理。"), SerializeField]
        private PlayerChooseCuisineData playerChooseCuisineData;
        
        
        
        [Header("便利貼設定"), Tooltip("用來顯示料理的便利貼的預製件。"), SerializeField]
        private GameObject stickyNotePrefab;
        
        [Tooltip("便利貼的生成位置。"), SerializeField]
        private Transform stickyNoteSpawnPoint;

        [Tooltip("- 便利貼生成數量。\n- 預設為 8。"), SerializeField]
        private int stickyNoteSpawnCount = 8;
        
        // 哪一個廚具開啟的介面的類，用於回傳烹飪哪一道料理用。
        private KitchenwareManager _kitchenwareManager;

        /// <summary>
        /// 用於初始化料理選擇介面的方法。
        /// </summary>
        /// <param name="kitchenwareManager"> 目標廚具的類。 </param>
        public void InitUI(KitchenwareManager kitchenwareManager)
        {
            _kitchenwareManager = kitchenwareManager;
            
            for (var i = 0; i < stickyNoteSpawnCount; i++)
            {
                var stickyNote = Instantiate(stickyNotePrefab, stickyNoteSpawnPoint);
                stickyNote.GetComponent<StickyNote>().InitStickyNote(this, playerChooseCuisineData.slotDataList[i]);
            }
        }

        /// <summary>
        /// 用於執行便利貼被按下時，所發生的事情的方法。
        /// </summary>
        /// <param name="chooseCuisineData"> 玩家所選擇的料理的資料。 </param>
        public void OnStickyNoteClick(Cuisine chooseCuisineData)
        {
            for (var i = 0; i < playerChooseCuisineData.slotDataList.Count; i++)
            {
                var selectSlotData = playerChooseCuisineData.slotDataList[i];

                // 如果兩道料理是不一樣的，就直接判斷下一道。
                if (!selectSlotData.cuisineData.Equals(chooseCuisineData))
                    continue;
                
                playerChooseCuisineData.slotDataList[i] = new PlayerChooseCuisineSlotData
                {
                    isLocked = selectSlotData.isLocked,
                    cuisineData = selectSlotData.cuisineData,
                    cuisineRemaining = selectSlotData.cuisineRemaining - 1
                };
                
                _kitchenwareManager.StartCook(chooseCuisineData);
                return;
            }
        }
    }
}
