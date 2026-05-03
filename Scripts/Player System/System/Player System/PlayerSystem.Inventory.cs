using Common.Item.Data;
using UI_System.Player_UI_System.Main;

namespace Player_System.System.Player_System
{
    public partial class PlayerSystem
    {
        private void PerformHotbar(int hotbarIndex)
        {
            PlayerUISystem.PerformHotbar(hotbarIndex);
        }

        public bool AddItem(IItem itemData)
        {
            return PlayerUISystem.AddItem(itemData);
        }

        public static bool RemoveItem(ItemSO itemData)
        {
            return PlayerUISystem.RemoveItem(itemData);
        }

        public void RemoveAllItems()
        {
            PlayerUISystem.RemoveAllItems();
        }
    }
}