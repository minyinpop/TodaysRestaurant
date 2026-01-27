using Common.Item;
using UI_System.Main;
using UnityEngine;

namespace Player_System.System.Child
{
    internal sealed class InventorySystem : MonoBehaviour
    {
        private void Start()
        {
            UISystem.InitializeHotbarUI();
        }
        
        public void PerformHotbar(int hotbarIndex)
        {
            UISystem.PerformHotbar(hotbarIndex);
        }

        public void PerformBackpack()
        {
            UISystem.PerformBackpack();
        }

        public bool TryAddItem(ItemSO itemData)
        {
            return UISystem.TryAddItem(itemData);
        }

        public bool TryRemoveItem(ItemSO itemData)
        {
            return UISystem.TryRemoveItem(itemData);
        }
    }
}