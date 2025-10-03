using System.Input.Main;
using UnityEngine;
using UnityEngine.UI;

namespace System.Inventory.Item_Drag
{
    internal sealed class ItemDragUI : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Image Image;
        
        private void LateUpdate()
        {
            InputSystem.GetMousePosition(out var position);
            transform.position = position;
        }
        
        public void SetSprite(Sprite itemSprite)
        {
            Image.sprite = itemSprite;
        }
    }
}