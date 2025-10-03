using UnityEngine;
using UnityEngine.InputSystem;

namespace System.Input.Main
{
    internal sealed class InputSystem : MonoBehaviour
    {
        private static GameObject Instance;

        private static InputManager InputManager;

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = gameObject;
            DontDestroyOnLoad(gameObject);
            
            InputManager = new InputManager();
        }

        private void OnEnable()
        {
            InputManager.Enable();
            InputManager.Mouse.LeftButton.performed += OnMouseLeftButtonClicked;
        }

        private void OnDisable()
        {
            InputManager.Mouse.LeftButton.performed -= OnMouseLeftButtonClicked;
            InputManager.Disable();
        }
        
        #region Mouse
        public static event Action MouseLeftButtonClicked;
        private void OnMouseLeftButtonClicked(InputAction.CallbackContext context)
        {
            MouseLeftButtonClicked?.Invoke();
        }

        public static void GetMousePosition(out Vector2 position)
        {
            position = InputManager.Mouse.MousePosition.ReadValue<Vector2>();
        }
        #endregion
    }
}