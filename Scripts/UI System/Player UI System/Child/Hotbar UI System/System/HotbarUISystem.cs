using Common.Item;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Hotbar_UI_System.System
{
    public sealed class HotbarUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private HotbarUI hotbarUI;
        
        private void Awake()
        {
            if (hotbarUI == null)
            {
                Debug.Log($"{nameof(HotbarUISystem)} > {nameof(hotbarUI)} cannot be null.");
            }
        }
        
        #region Input
            public void UseSelectedHotbarSlotItem()
            {
                hotbarUI.UseSelectedHotbarSlotItem();
            }
            
            public void PerformHotbar(int hotbarIndex)
            {
                hotbarUI.PerformHotbar(hotbarIndex);
            }
        #endregion
        
        public bool TryAddItem(ItemSO itemData)
        {
            return hotbarUI.TryAddItem(itemData);
        }

        public bool TryRemoveItem(ItemSO itemData)
        {
            return hotbarUI.TryRemoveItem(itemData);
        }
    }
}