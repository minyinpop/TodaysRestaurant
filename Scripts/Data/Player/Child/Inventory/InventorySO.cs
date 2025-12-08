using Data.Item.Abstract;
using UnityEngine;

namespace Data.Player.Child.Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory Data", fileName = "New Data")]
    internal sealed class InventorySO : ScriptableObject
    {
        private ItemSO[] HotbarItemsData = new ItemSO[10];

        public void SetItem()
        {
        }
    }
}