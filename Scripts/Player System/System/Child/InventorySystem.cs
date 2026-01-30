using Common.Item;
using UI_System.Player_UI_System.Main;
using UnityEngine;

namespace Player_System.System.Child
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

        public bool TryAddItem(ItemSO itemData)
        {
            return PlayerUISystem.TryAddItem(itemData);
        }

        public bool TryRemoveItem(ItemSO itemData)
        {
            return PlayerUISystem.TryRemoveItem(itemData);
        }
    }
}