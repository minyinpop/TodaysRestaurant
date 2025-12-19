using Item.Data;
using Player_System.System.Child.Inventory.Child;
using UnityEngine;

namespace Player_System.System.Child.Inventory.Main
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

        public void OnClickedMouseLeftButton()
        {
            Hotbar.OnClickedMouseLeftButton();
        }

        public bool TryAddItem(ItemSO item)
        {
            Hotbar.TryAddItem(item, out var isSuccess);
            return isSuccess;
        }
    }
}