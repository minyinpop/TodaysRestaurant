using System.Collections.Generic;
using Item.Category.Cuisine;
using Menu.Slot;
using Player.Menu;
using UnityEngine;

namespace Menu.Page.Category
{
    /// <summary>
    /// 用於管理菜單頁面的右半邊，專門顯示玩家當前上架了甚麼菜品。
    /// 與 MenuManager 這個類為綁定狀態。
    /// </summary>
    public class MenuChooseCuisineMenuPage : MenuPageBase
    {
        // ====================================================================================================
        // 用於暫存被選擇的菜品的格子的陣列。
        // 因為在 OnDisable() 中，需要回傳裡面的資料到 MenuData。
        // 不用即時更新是因為這樣太麻煩，所以先把選擇好的菜品資料，先存在 MenuChooseCuisineSlot 裡，
        // 等 Unity 呼叫 OnDisable() 後，在遍歷整個陣列，並獲取格子裡暫存的資料。
        // ====================================================================================================
        private List<GameObject> _slotList = new List<GameObject>();
        
        /// <summary>
        /// 用於初始化頁面的的方法。
        /// </summary>
        public override void InitPage(MenuData newMenuData)
        {
            MenuData = newMenuData;

            for (var i = 0; i < MenuData.ChooseCuisineData.SlotDataList.Count; i++)
            {
                var selectedSlotData = MenuData.ChooseCuisineData.SlotDataList[i];
                
                _slotList.Add(Instantiate(slotPrefab, slotSpawnPoint));
                _slotList[i].GetComponent<MenuChooseCuisineSlot>().Refresh(selectedSlotData);
            }
        }

        /// <summary>
        /// 添加菜品資料到格子中。
        /// </summary>
        /// <param name="newCuisineData"> 被添加的菜品的資料。 </param>
        public override void AddCuisineData(Cuisine newCuisineData)
        {
            foreach (var slot in _slotList)
            {
                if (slot.GetComponent<MenuChooseCuisineSlot>().AddSlotData(newCuisineData))
                    return;
            }
        }
    }
}
