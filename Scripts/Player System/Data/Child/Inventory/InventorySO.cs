using Item;
using Item.Data;
using UnityEngine;

namespace Player_System.Data.Child.Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory Data", fileName = "New Data")]
    internal sealed class InventorySO : ScriptableObject
    {
        private ItemSO[] HotbarItemsData = new ItemSO[10];
    }
}