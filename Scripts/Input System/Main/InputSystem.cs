using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input_System.Main
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
            
            #region Hotbar
                InputManager.Hotbar.One.started += OnOneHotbarStarted;
                InputManager.Hotbar.Two.started += OnTwoHotbarStarted;
                InputManager.Hotbar.Three.started += OnThreeHotbarStarted;
            #endregion
            
            #region Mouse
                InputManager.Mouse.LeftButton.started += OnMouseLeftButtonClicked;
            #endregion
            
            #region Player
                InputManager.Player.Walk.started += OnPlayerWalkStarted;
                InputManager.Player.Walk.canceled += OnPlayerWalkCanceled;
            #endregion
        }

        private void OnDisable()
        {
            #region Hotbar
                InputManager.Hotbar.One.started -= OnOneHotbarStarted;
                InputManager.Hotbar.Two.started -= OnTwoHotbarStarted;
                InputManager.Hotbar.Three.started -= OnThreeHotbarStarted;
            #endregion
            
            #region Mouse
                InputManager.Mouse.LeftButton.started -= OnMouseLeftButtonClicked;
            #endregion
            
            #region Player
                InputManager.Player.Walk.started -= OnPlayerWalkStarted;
                InputManager.Player.Walk.canceled -= OnPlayerWalkCanceled;
            #endregion
            
            InputManager.Disable();
        }
        
        #region Hotbar
            public static event Action OnStartedOneHotbar;
            private static void OnOneHotbarStarted(InputAction.CallbackContext context) => OnStartedOneHotbar?.Invoke();
            public static event Action OnStartedTwoHotbar;
            private static void OnTwoHotbarStarted(InputAction.CallbackContext context) => OnStartedTwoHotbar?.Invoke();
            public static event Action OnStartedThreeHotbar;
            private static void OnThreeHotbarStarted(InputAction.CallbackContext context) => OnStartedThreeHotbar?.Invoke();
        #endregion
        
        #region Mouse
            public static event Action OnClickMouseLeftButton;
            private void OnMouseLeftButtonClicked(InputAction.CallbackContext context) => OnClickMouseLeftButton?.Invoke();
            public static void GetMousePosition(out Vector2 position) => position = InputManager.Mouse.MousePosition.ReadValue<Vector2>();
        #endregion
        
        #region Player
            #region State
                public static event Action OnStartedPlayerWalk;
                private static void OnPlayerWalkStarted(InputAction.CallbackContext context) => OnStartedPlayerWalk?.Invoke();
                public static event Action OnCancelPlayerWalk;
                private static void OnPlayerWalkCanceled(InputAction.CallbackContext context) => OnCancelPlayerWalk?.Invoke();
            #endregion
            
            #region Value
                public static void GetPlayerWalkDirection(out Vector2 direction) => direction = InputManager.Player.Walk.ReadValue<Vector2>();
            #endregion
        #endregion
    }
}