using Common.Item.Data;
using UI_System.Player_UI_System.Main;

namespace Player_System.System
{
    public partial class PlayerSystem
    {
        private void PerformHotbar(int hotbarIndex)
        {
            PlayerUISystem.PerformHotbar(hotbarIndex);
        }

        private void RequireBackpackUI()
        {
            PlayerUISystem.RequireBackpackUI();
        }

        public bool TryAddItem(IItem itemData)
        {
            return PlayerUISystem.TryAddItem(itemData);
        }

        public static bool TryRemoveItem(ItemSO itemData)
        {
            return PlayerUISystem.TryRemoveItem(itemData);
        }
    }
}