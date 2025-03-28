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
    }
}
