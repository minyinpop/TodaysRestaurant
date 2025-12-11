using Input_System.Main;
using UnityEngine;
using UnityEngine.UI;

namespace Mouse_System.Child.Item.Object
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