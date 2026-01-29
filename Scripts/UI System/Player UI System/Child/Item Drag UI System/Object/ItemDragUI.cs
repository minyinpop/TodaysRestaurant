using UnityEngine;
using UnityEngine.UI;

namespace UI_System.Player_UI_System.Child.Item_Drag_UI_System.Object
{
    public sealed class ItemDragUI : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Image itemImage;

        private void Awake()
        {
            if (itemImage == null)
            {
                Debug.Log($"{nameof(ItemDragUI)} > {nameof(itemImage)} cannot be null.");
            }
        }

        public void SetItemImage(Sprite sprite)
        {
            if (sprite == null)
            {
                Debug.Log($"{nameof(ItemDragUI)} > {nameof(SetItemImage)} > {nameof(sprite)} cannot be null.");
                return;
            }

            itemImage.sprite = sprite;
        }

        public void ClearItemImage()
        {
            itemImage.sprite = null;
        }
    }
}