using Item;
using UnityEngine;

namespace UI_System.System.Child.Hotbar_UI_System
{
    public sealed class HotbarUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject uiPrefab;
        [field: SerializeField] private RectTransform uiParent;
        
        private GameObject _ui;
        private HotbarUI _ui_HotbarUI;
        
        public void RequiresUI(bool show)
        {
            if (_ui is null)
            {
                _ui = Instantiate(uiPrefab, uiParent);
                _ui_HotbarUI = _ui.GetComponent<HotbarUI>();
                _ui.SetActive(true);
            }
            else
            {
                _ui.SetActive(_ui.activeSelf);
            }
        }
        
        public bool TryAddItem(ItemSO itemData)
        {
            return _ui_HotbarUI.TryAddItem(itemData);
        }

        public void PerformHotbar(int hotbarIndex)
        {
            _ui_HotbarUI.PerformHotbar(hotbarIndex);
        }

        public void ClickRightButton()
        {
            _ui_HotbarUI.ClickRightButton();
        }
    }
}