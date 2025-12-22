using System;
using Item;
using UnityEngine;

namespace Player_System.System.Child
{
    internal sealed class InventorySystem : MonoBehaviour
    {
        public static event Action<bool> HotbarUIRequired;
        public static event Action<int> HotbarPerformed;
        public static event Func<ItemSO, bool> TryItemAdded;

        private void OnEnable()
        {
            HotbarUIRequired?.Invoke(true);
        }

        private void OnDisable()
        {
            HotbarUIRequired?.Invoke(false);
        }

        public void PerformHotbar(int hotbarIndex)
        {
            HotbarPerformed?.Invoke(hotbarIndex);
        }

        public bool TryAddItem(ItemSO item)
        {
            return TryItemAdded?.Invoke(item) ?? false;
        }
    }
}