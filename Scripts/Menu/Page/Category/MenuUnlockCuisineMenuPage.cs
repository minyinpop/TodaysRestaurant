using DataBase.Menu;
using Menu.Slot;

namespace Menu.Page.Category
{
    /// <summary>
    /// 用於管理菜單頁面的左半邊，專門顯示玩家當前的類別，有解鎖甚麼菜品。
    /// 與 MenuManager 這個類為綁定狀態。
    /// </summary>
    public class MenuUnlockCuisineMenuPage : MenuPageBase
    {        
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
            MenuData = newMenuData;

            foreach (var slot in SlotList)
                Destroy(slot);
            
            SlotList.Clear();
            InitSlot();
        }

        /// <summary>
        /// 生成格子用的方法。
        /// </summary>
        private void InitSlot()
        {
            foreach (var cuisine in MenuData.UnlockCuisineData.CuisineList)
            {
                var slot = Instantiate(slotPrefab, slotSpawnPoint);
                slot.GetComponent<MenuUnlockCuisineSlot>().Refresh(cuisine);
                SlotList.Add(slot);
            }
        }
    }
}
