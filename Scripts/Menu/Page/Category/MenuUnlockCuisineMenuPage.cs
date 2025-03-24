using Menu.Slot;
using Player.Menu;

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

            foreach (var cuisine in MenuData.UnlockCuisineData.CuisineList)
            {
                var slot = Instantiate(slotPrefab, slotSpawnPoint);
                slot.GetComponent<MenuUnlockCuisineSlot>().Refresh(cuisine);
            }
        }
    }
}
