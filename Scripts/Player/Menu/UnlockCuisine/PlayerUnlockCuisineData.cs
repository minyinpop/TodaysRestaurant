using System.Collections.Generic;
using Item.Category.Cuisine;
using UnityEngine;

namespace Player.Menu.UnlockCuisine
{
    /// <summary>
    /// 用來儲存玩家解鎖了哪幾道的料理，以料理種類為分類，像是主菜、飲料等等。
    /// </summary>
    [CreateAssetMenu(fileName = "New Player Unlock Cuisine Data", menuName = "Player/Menu/Unlock Cuisine Data", order = 2)]
    public class PlayerUnlockCuisineData : ScriptableObject
    {
        [field: Header("資料庫"), Tooltip("- 此為解鎖的料理。\n- 用於菜單選擇上架菜品用的。"), SerializeField]
        public List<Cuisine> CuisineList { get; private set; }
    }
}
