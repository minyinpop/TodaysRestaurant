using System.Collections.Generic;
using DataBase.Item.Category.Cuisine;
using UnityEngine;

namespace DataBase.Menu.ChooseCuisine
{
    /// <summary>
    /// 用來記錄玩家選擇了甚麼料理做販售，僅限於當天的營業。
    /// 裡面包含了玩家選擇了甚麼料理，以及料理剩餘的份數。
    /// </summary>
    [CreateAssetMenu(fileName = "New Player Choose Cuisine Data", menuName = "Player/Menu/Choose Cuisine Data", order = 3)]
    public class PlayerChooseCuisineData : ScriptableObject
    {
        [Header("當日上架的料理資料"), Tooltip("- 此為格子的資料。\n- 多少筆資料代表會生成多少的格子。\n- 請不要生成超過 8 筆資料。")]
        public List<PlayerChooseCuisineSlotData> slotDataList;

        /// <summary>
        /// 用於重置資料庫的方法。
        /// 以防下次開啟菜單時，出現殘留的資料。
        /// </summary>
        public void Clear()
        {
            for (var i = 0; i < slotDataList.Count; i++)
            {
                slotDataList[i] = new PlayerChooseCuisineSlotData
                {
                    isLocked = slotDataList[i].isLocked,
                    cuisineData = null,
                    cuisineRemaining = 0
                };
            }
        }
    }

    /// <summary>
    /// 用來當作 PlayerChooseCuisineSlot 的資料，裡面有兩筆資料。
    /// </summary>
    [System.Serializable]
    public struct PlayerChooseCuisineSlotData
    {
        // 該格子是否是上鎖狀態的。
        public bool isLocked;
        
        // 該格子裡所暫存的料理的資料。
        public Cuisine cuisineData;

        // 該格子裡所暫存的料理的數量。
        public int cuisineRemaining;
    }
}
