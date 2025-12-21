using Input_System.Main;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace UI_System.System.Child
{
    public sealed class ItemDragUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject prefab;
        [field: SerializeField] private RectTransform parent;

        private GameObject _tempUI;
        private RectTransform _tempUI_Rect;
        private Image _tempUI_Image;

        private void FixedUpdate()
        {
            if (_tempUI is null) return;
            _tempUI_Rect.anchoredPosition = InputSystem.MousePosition();
        }

        public void RequiresUI(bool isDragging, ItemSO item)
        {
            if (isDragging)
            {
                if (_tempUI is null)
                {
                    _tempUI = Instantiate(prefab, parent);
                    _tempUI_Rect = _tempUI.GetComponent<RectTransform>();
                    _tempUI_Image = _tempUI.GetComponent<Image>();
                }
                
                _tempUI_Image.sprite = item.ItemSprite;
            }
            else
            {
                if (_tempUI is null) return;
                Destroy(_tempUI);
                _tempUI = null;
                _tempUI_Rect = null;
                _tempUI_Image = null;
            }
        }
    }
}