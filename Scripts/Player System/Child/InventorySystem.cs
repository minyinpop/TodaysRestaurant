using Common.Item.Data;
using UI_System.Player_UI_System.Main;
using UnityEngine;

namespace Player_System.Child
{
    internal sealed class InventorySystem : MonoBehaviour
    {
        public void PerformHotbar(int hotbarIndex)
        {
            PlayerUISystem.PerformHotbar(hotbarIndex);
        }

        public void RequireBackpackUI()
        {
            PlayerUISystem.RequireBackpackUI();
        }

        public bool TryAddItem(IItem itemData)
        {
            return PlayerUISystem.TryAddItem(itemData);
        }

        public bool TryRemoveItem(ItemSO itemData)
        {
            return PlayerUISystem.TryRemoveItem(itemData);
        }
    }
}