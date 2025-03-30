using System.Collections.Generic;
using DataBase.Menu.ChooseCuisine;
using DataBase.Player.Cuisine_Deliver;
using UnityEngine;

namespace Tool
{
    /// <summary>
    /// 用於在營業結束，或是關閉遊戲時，所重置資料庫的類。
    /// 防止資料庫裡的資料殘留，而引發意外的問題。
    /// </summary>
    public class ResetDataOnDestroy : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("玩家在運送料理的資料庫。"), SerializeField]
        private PlayerCuisineDeliverData playerCuisineDeliverData;
        
        [Tooltip("本日上架的料理的資料庫。"), SerializeField]
        private List<PlayerChooseCuisineData> playerChooseCuisineDataList;
        
        private void OnDestroy()
        {
            playerCuisineDeliverData.Clear();
            
            foreach (var playerChooseCuisine in playerChooseCuisineDataList)
                playerChooseCuisine.Clear();
        }
    }
}
