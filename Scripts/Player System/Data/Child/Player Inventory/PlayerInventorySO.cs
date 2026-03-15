using Common.Item.Data;
using UnityEngine;

namespace Player_System.Data.Child.Player_Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory", fileName = "New Data")]
    internal sealed class PlayerInventorySO : ScriptableObject
    {
        private ItemSO[] HotbarItemsData = new ItemSO[10];
    }
}