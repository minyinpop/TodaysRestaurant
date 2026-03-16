using Common.Item.Data;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Hotbar_UI_System.System
{
    public sealed class HotbarUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private HotbarUI hotbarUI;

        private bool _isHotbarEnabled = true;
        
        private void Awake()
        {
            if (hotbarUI is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(hotbarUI)} cannot be null.");
                Destroy(gameObject);
            }
            
            SetHotbarUI(_isHotbarEnabled);
        }

        public void SetHotbarUI(bool isEnabled)
        {
            _isHotbarEnabled = isEnabled;
            hotbarUI.gameObject.SetActive(_isHotbarEnabled);
        }

        #region Input
            public void UseSelectedHotbarSlotItem()
            {
                if (_isHotbarEnabled)
                {
                    hotbarUI.UseSelectedHotbarSlotItem();
                }
            }
            
            public void PerformHotbar(int hotbarIndex)
            {
                if (_isHotbarEnabled)
                {
                    hotbarUI.PerformHotbar(hotbarIndex);
                }
            }
        #endregion
        
        public bool TryAddItem(IItem itemData)
        {
            if (_isHotbarEnabled)
            {
                return hotbarUI.TryAddItem(itemData);
            }
            else
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(TryAddItem)} > hotbar is disabled.");
                return false;
            }
        }

        public bool TryRemoveItem(ItemSO itemData)
        {
            if (_isHotbarEnabled)
            {
                return hotbarUI.TryRemoveItem(itemData);
            }
            else
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(TryRemoveItem)} > hotbar is disabled.");
                return false;
            }
        }
    }
}