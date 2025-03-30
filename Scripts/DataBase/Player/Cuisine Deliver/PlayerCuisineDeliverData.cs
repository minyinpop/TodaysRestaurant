using System.Collections.Generic;
using DataBase.Item.Category.Cuisine;
using UnityEngine;

namespace DataBase.Player.Cuisine_Deliver
{
    /// <summary>
    /// 用來當作玩家運送料理的資料庫。
    /// </summary>
    [CreateAssetMenu(fileName = "New Player Unlock Cuisine Data", menuName = "Player/Cuisine Deliver/Cuisine Deliver Data", order = 1)]
    public class PlayerCuisineDeliverData : ScriptableObject
    {
        [Header("料理資料陣列"), Tooltip("- 用於儲存料理資料的陣列。\n- 數量與玩家頭上的圖片數量一致。"), SerializeField]
        public List<Cuisine> cuisineDataList = new List<Cuisine>();

        /// <summary>
        /// 用於外部獲取料理的方法。
        /// 獲取道料理資料後，就會同步刪除被選中的料理的資料。
        /// </summary>
        /// <returns> 回傳最上方的料理。 </returns>
        public Cuisine GetCuisineData()
        {
            for (var i = cuisineDataList.Count - 1; i >= 0; i--)
            {
                var selectCuisineData = cuisineDataList[i];
                
                if (selectCuisineData is null)
                    continue;

                cuisineDataList.RemoveAt(i);
                cuisineDataList.Add(null);
                return selectCuisineData;
            }

            return null;
        }

        /// <summary>
        /// 用於把料理資料陣列給全部重置的方法。
        /// </summary>
        public void Clear()
        {
            for (var i = 0; i < cuisineDataList.Count; i++)
                cuisineDataList[i] = null;
        }
    }
}
