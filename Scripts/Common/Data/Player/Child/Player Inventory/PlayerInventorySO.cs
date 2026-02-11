using Common.Data.Item;
using UnityEngine;

namespace Common.Data.Player.Child.Player_Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory", fileName = "New Data")]
    internal sealed class PlayerInventorySO : ScriptableObject
    {
        private ItemSO[] HotbarItemsData = new ItemSO[10];
    }
}