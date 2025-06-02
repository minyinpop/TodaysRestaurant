using System.Collections.Generic;
using Storage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utility;
using InputSystem = Input.InputSystem;

namespace Player
{
    internal class PlayerItemDragHandler : MonoBehaviour
    {
        [field: Header("必要組件")]
        [field: SerializeField] private GraphicRaycaster Raycaster { get; set; }
        private EventSystem EventSystem { get; set; }
        
        private InputManager Input { get; set; }
        private Vector2 MousePos => Input.Mouse.Position.ReadValue<Vector2>();
        
        private void Awake()
        {
            Input = InputSystem.Input;
            EventSystem = EventSystem.current;
            
            if (Tools.CheckNull(Input is null, $"錯誤訊息：找尋不到 InputSystem。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n") ||
                Tools.CheckNull(!Raycaster, $"錯誤訊息：Raycaster 未被掛載。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n") ||
                Tools.CheckNull(EventSystem is null, $"錯誤訊息：找尋不到 EventSystem。\n遊戲物件：{name}\n錯誤組件：{GetType().Name}\n")) enabled = false;
        }

        private void OnEnable() => Input.Mouse.LeftClick.started += OnLeftButtonClick;
        private void OnDisable() => Input.Mouse.LeftClick.started -= OnLeftButtonClick;

        private void OnLeftButtonClick(InputAction.CallbackContext context)
        {
            var pointerEventData = new PointerEventData(EventSystem) { position = MousePos };
            var results = new List<RaycastResult>();
            Raycaster.Raycast(pointerEventData, results);
            if (results.Count <= 0) return;
            if (!results[0].gameObject.TryGetComponent<IStorageSlot>(out var storageSlot)) return;
            Debug.Log(results[0].gameObject.name);
        }
    }
}