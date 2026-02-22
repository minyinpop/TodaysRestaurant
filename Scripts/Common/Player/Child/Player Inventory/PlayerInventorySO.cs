using Common.Item.Data;
using UnityEngine;

namespace Common.Player.Child.Player_Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory", fileName = "New Data")]
    internal sealed class PlayerInventorySO : ScriptableObject
    {
        private ItemSO[] HotbarItemsData = new ItemSO[10];
    }
}