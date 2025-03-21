using Item.Category.Cuisine;
using UnityEngine;

namespace Player.Menu.ChooseCuisine
{
    /// <summary>
    /// 用來記錄玩家選擇了甚麼菜品做販售，僅限於當天的營業。
    /// </summary>
    [CreateAssetMenu(fileName = "New Player Choose Cuisine Data", menuName = "Player/Menu/Choose Cuisine Data", order = 3)]
    public class PlayerChooseCuisineData : ScriptableObject
    {
        [field: Header("資料庫"), Tooltip("- 此為解鎖的格子數量。\n- 最多可以解鎖 8 格的上架空間。"), SerializeField]
        public int SlotQuantity { get; private set; }
        
        [field: Tooltip("- 此為格子裡的料理的資料。\n- 數量"), SerializeField]
        public Cuisine[] CuisineList { get; private set; }
    }
}
