using Data.Item.Base;
using UnityEngine;

namespace Data.Player.Child.Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory Data", fileName = "New Data")]
    internal sealed class InventorySO : ScriptableObject
    {
        [field: Header("Item Data")]
        [field: SerializeField] private ItemSO[] HotbarItemsData;
        // TODO 背包的資料
    }
}