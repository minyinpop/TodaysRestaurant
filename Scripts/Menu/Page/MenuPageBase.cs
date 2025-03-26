using System.Collections.Generic;
using DataBase.Item.Category.Cuisine;
using DataBase.Menu;
using UnityEngine;

namespace Menu.Page
{
    public abstract class MenuPageBase : MenuManager
    {
        [Header("格子相關"), Tooltip("- 用來生成格子用的預製件。\n- 每個頁面都有不同的預製件。"), SerializeField]
        protected GameObject slotPrefab;
        
        [Tooltip("格子的生成位置。"), SerializeField]
        protected Transform slotSpawnPoint;
        
        // 當前玩家選擇的蔡品種類的資料暫存。
        protected MenuData MenuData;
        
        // 格子的暫存陣列，用於在更新頁面時，刪除格子用。
        protected List<GameObject> SlotList = new();

        /// <summary>
        /// 用於初始化頁面的的方法。
        /// </summary>
        /// <param name="newMenuData"> 新傳入的菜單介面資料。 </param>
        public abstract void InitPage(MenuData newMenuData);
        
        /// <summary>
        /// 用於更新整個介面的方法。
        /// </summary>
        /// <param name="newMenuData"> 新傳入的菜單介面資料。 </param>
        public abstract void Refresh(MenuData newMenuData);
        
        /// <summary>
        /// 添加菜品資料到格子中。
        /// 此方法為 MenuChooseCuisineMenuPage 做使用。
        /// </summary>
        /// <param name="newCuisineData"> 被添加的菜品的資料。 </param>
        public virtual void AddCuisineData(Cuisine newCuisineData)
        {
        }
    }
}
