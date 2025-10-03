using UnityEngine;

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
        }

        private void OnDisable()
        {
            InputManager.Disable();
        }
        
        #region Mouse
        public static void GetMousePosition(out Vector2 position)
        {
            position = InputManager.Mouse.MousePosition.ReadValue<Vector2>();
        }
        #endregion
    }
}