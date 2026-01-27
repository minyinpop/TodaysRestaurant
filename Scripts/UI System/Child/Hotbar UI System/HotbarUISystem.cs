using Common.Item;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Hotbar_UI_System
{
    public sealed class HotbarUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject uiPrefab;
        [field: SerializeField] private RectTransform uiParent;
        
        private HotbarUI _ui;
        
        public void Initialize()
        {
            if (_ui is not null) return;
            _ui = Instantiate(uiPrefab, uiParent).GetComponent<HotbarUI>();
            _ui.gameObject.SetActive(true);
        }
        
        #region Input
            public void ClickRightButton()
            {
                _ui.ClickRightButton();
            }
            
            public void PerformHotbar(int hotbarIndex)
            {
                _ui.PerformHotbar(hotbarIndex);
            }
        #endregion
        
        public bool TryAddItem(ItemSO itemData)
        {
            return _ui.TryAddItem(itemData);
        }

        public bool TryRemoveItem(ItemSO itemData)
        {
            return _ui.TryRemoveItem(itemData);
        }
    }
}