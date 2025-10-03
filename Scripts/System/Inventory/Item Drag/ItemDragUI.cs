using System.Input.Main;
using Data.Item.Base;
using UnityEngine;
using UnityEngine.UI;

namespace System.Inventory.Item_Drag
{
    internal sealed class ItemDragUI : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Image Image;
        
        private ItemSO ItemData;

        private void LateUpdate()
        {
            InputSystem.GetMousePosition(out var position);
            transform.position = position;
        }
        
        public void Add(ItemSO item)
        {
            item.GetItemSprite(out var sprite);
            Image.sprite = sprite;
        }
    }
}