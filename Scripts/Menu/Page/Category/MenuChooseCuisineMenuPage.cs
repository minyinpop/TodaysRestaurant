using DataBase.Item.Category.Cuisine;
using DataBase.Menu;
using Menu.Slot;

namespace Menu.Page.Category
{
    /// <summary>
    /// 用於管理菜單頁面的右半邊，專門顯示玩家當前上架了甚麼菜品。
    /// 與 MenuManager 這個類為綁定狀態。
    /// </summary>
    public class MenuChooseCuisineMenuPage : MenuPageBase
    {
        private void OnDestroy()
        {
            // 遍歷整個 _slotList，並把格子中的暫存資料給儲存進資料庫裡。
            for (var i = 0; i < SlotList.Count; i++)
            {
                MenuData.ChooseCuisineData.slotDataList[i] = SlotList[i].GetComponent<MenuChooseCuisineSlot>().SlotData;
                Destroy(SlotList[i]);
            }
        }
        
        /// <summary>
        /// 用於初始化頁面的的方法。
        /// </summary>
        public override void InitPage(MenuData newMenuData)
        {
            MenuData = newMenuData;
            InitSlot();
        }
        
        /// <summary>
        /// 用於更新整個介面的方法。
        /// </summary>
        /// <param name="newMenuData"> 新傳入的菜單介面資料。 </param>
        public override void Refresh(MenuData newMenuData)
        {
            // 遍歷整個 _slotList，並把格子中的暫存資料給儲存進資料庫裡。
            for (var i = 0; i < SlotList.Count; i++)
            {
                MenuData.ChooseCuisineData.slotDataList[i] = SlotList[i].GetComponent<MenuChooseCuisineSlot>().SlotData;
                Destroy(SlotList[i]);
            }

            SlotList.Clear();
            
            MenuData = newMenuData;
            InitSlot();
        }

        /// <summary>
        /// 添加菜品資料到格子中。
        /// </summary>
        /// <param name="newCuisineData"> 被添加的菜品的資料。 </param>
        public override void AddCuisineData(Cuisine newCuisineData)
        {
            foreach (var slot in SlotList)
            {
                if (slot.GetComponent<MenuChooseCuisineSlot>().AddSlotData(newCuisineData))
                    return;
            }
        }
        
        /// <summary>
        /// 生成格子用的方法。
        /// </summary>
        private void InitSlot()
        {
            for (var i = 0; i < MenuData.ChooseCuisineData.slotDataList.Count; i++)
            {
                var selectedSlotData = MenuData.ChooseCuisineData.slotDataList[i];
                
                SlotList.Add(Instantiate(slotPrefab, slotSpawnPoint));
                SlotList[i].GetComponent<MenuChooseCuisineSlot>().Refresh(selectedSlotData);
            }
        }
    }
}
