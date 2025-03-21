using Player.Menu.ChooseCuisine;
using Player.Menu.UnlockCuisine;
using UnityEngine;

namespace Player.Menu
{
    /// <summary>
    /// 用來儲存 PlayerUnlockCuisineData 與 PlayerChooseCuisineData 這兩個類。
    /// </summary>
    [CreateAssetMenu(fileName = "New Menu Data", menuName = "Player/Menu/Menu Data", order = 1)]
    public class MenuData : ScriptableObject
    {
        [field: Header("資料庫"), Tooltip("玩家已解鎖的菜品的資料庫。"), SerializeField]
        public PlayerUnlockCuisineData UnlockCuisineData { get; private set; }
        
        [field: Tooltip("玩家選擇上架的蔡品的資料庫。"), SerializeField]
        public PlayerChooseCuisineData ChooseCuisineData { get; private set; }
    }
}
