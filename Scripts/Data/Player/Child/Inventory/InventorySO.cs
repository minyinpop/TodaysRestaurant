using Data.Item.Interface;
using UnityEngine;

namespace Data.Player.Child.Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory Data", fileName = "New Data")]
    internal sealed class InventorySO : ScriptableObject
    {
        private ITem[] HotbarItemsData = new ITem[10];
    }
}