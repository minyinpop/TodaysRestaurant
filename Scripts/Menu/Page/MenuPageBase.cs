using Item.Category.Cuisine;
using Player.Menu;
using UnityEngine;

namespace Menu.Page
{
    public abstract class MenuPageBase : MonoBehaviour
    {
        [Header("格子相關"), Tooltip("- 用來生成格子用的預製件。\n- 每個頁面都有不同的預製件。"), SerializeField]
        protected GameObject slotPrefab;
        
        [Tooltip("格子的生成位置。"), SerializeField]
        protected Transform slotSpawnPoint;
        
        // 當前玩家選擇的蔡品種類的資料暫存。
        protected MenuData MenuData;

        /// <summary>
        /// 用於初始化頁面的的方法。
        /// </summary>
        public abstract void InitPage(MenuData newMenuData);
        
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
