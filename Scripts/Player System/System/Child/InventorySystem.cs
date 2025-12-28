using System;
using Item;
using UI_System.System.Main;
using UnityEngine;

namespace Player_System.System.Child
{
    internal sealed class InventorySystem : MonoBehaviour
    {
        private void OnEnable()
        {
            UISystem.RequireHotbarUI();
        }

        private void OnDisable()
        {
            UISystem.RequireHotbarUI();
        }

        public void PerformHotbar(int hotbarIndex)
        {
            UISystem.PerformHotbar(hotbarIndex);
        }

        public bool TryAddItem(ItemSO item)
        {
            return UISystem.TryAddItem(item);
        }
    }
}