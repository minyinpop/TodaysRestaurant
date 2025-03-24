using System.Collections.Generic;
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
        [field: Tooltip("- 此為格子的資料。\n- 多少筆資料代表會生成多少的格子。\n- 請不要生成超過 8 筆資料。"), SerializeField]
        public List<PlayerChooseCuisineSlotData> SlotDataList { get; private set; }
    }

    /// <summary>
    /// 用來當作 PlayerChooseCuisineSlot 的資料，裡面有兩筆資料。
    /// </summary>
    [System.Serializable]
    public struct PlayerChooseCuisineSlotData
    {
        // 該格子是否是上鎖狀態的。
        public bool isLocked;
        
        // 該格子裡所暫存的料理的資料。
        public Cuisine cuisineData;
    }
}
