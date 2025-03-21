using System.Collections.Generic;
using Item.Category.Cuisine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Menu
{
    public class MenuCuisineSelectSystem : MonoBehaviour
    {
        // 
        private Cuisine _cuisineData;

        private void OnEnable()
        {
            InputSystem.input.Mouse.LeftClick.performed += OnLeftButtonClick;
        }

        private void OnDisable()
        {
            InputSystem.input.Mouse.LeftClick.performed -= OnLeftButtonClick;
        }

        private void OnLeftButtonClick(InputAction.CallbackContext context)
        {
        }

        private void SlotDetect()
        {
            var pointer = new PointerEventData(EventSystem.current)
            {
                position = InputSystem.MousePos()
            };
            var results = new List<RaycastResult>();

            foreach (var result in results)
            {
            }
        }
    }
}
