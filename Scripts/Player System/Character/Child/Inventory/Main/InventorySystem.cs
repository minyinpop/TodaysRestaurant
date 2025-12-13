using Item;
using Player_System.Character.Child.Inventory.Child;
using UnityEngine;

namespace Player_System.Character.Child.Inventory.Main
{
    internal sealed class InventorySystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private Hotbar Hotbar;
        [field: SerializeField] private Backpack Backpack;

        public void OnPerformedHotbar(int hotbarIndex)
        {
            Hotbar.OnPerformedHotbar(hotbarIndex);
        }
        
        public bool TryAddItem(ItemSO item)
        {
            Hotbar.TryAddItem(item, out var isSuccess);
            return isSuccess;
        }
    }
}