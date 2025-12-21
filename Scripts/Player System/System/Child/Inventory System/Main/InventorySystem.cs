using Item;
using Player_System.System.Child.Inventory_System.Child;
using UnityEngine;

namespace Player_System.System.Child.Inventory_System.Main
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

        public void OnClickedLeftButton()
        {
            Hotbar.OnClickedLeftButton();
        }

        public bool TryAddItem(ItemSO item)
        {
            return Hotbar.TryAddItem(item);
        }
    }
}