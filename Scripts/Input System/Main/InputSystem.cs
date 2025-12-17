using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Input_System.Main
{
    public sealed class InputSystem : MonoBehaviour
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
            
            #region Mouse
                InputManager.Mouse.LeftButton.performed += OnMouseLeftButtonClicked;
            #endregion
            
            #region Player
                InputManager.Player.Walk.started += OnPlayerWalkStarted;
                InputManager.Player.Walk.canceled += OnPlayerWalkCanceled;
                InputManager.Player.Hotbar.performed += OnHotbarPerformed;
            #endregion
        }

        private void OnDisable()
        {
            #region Mouse
                InputManager.Mouse.LeftButton.performed -= OnMouseLeftButtonClicked;
            #endregion
            
            #region Player
                InputManager.Player.Walk.started -= OnPlayerWalkStarted;
                InputManager.Player.Walk.canceled -= OnPlayerWalkCanceled;
                InputManager.Player.Hotbar.performed -= OnHotbarPerformed;
            #endregion
            
            InputManager.Disable();
        }
        
        #region Player
            #region Walk
                public static event Action OnStartedPlayerWalk;
                private static void OnPlayerWalkStarted(InputAction.CallbackContext context) => OnStartedPlayerWalk?.Invoke();
                public static event Action OnCancelPlayerWalk;
                private static void OnPlayerWalkCanceled(InputAction.CallbackContext context) => OnCancelPlayerWalk?.Invoke();
                
                public static void GetPlayerWalkDirection(out Vector2 direction) => direction = InputManager.Player.Walk.ReadValue<Vector2>();
            #endregion
            
            #region Hotbar
                public static event Action<int> OnPerformedHotbar;
                private static void OnHotbarPerformed(InputAction.CallbackContext context)
                {
                    if (context.control is not KeyControl key) return;
                    var index = key.keyCode switch
                    {
                        Key.Digit1 => 0,
                        Key.Digit2 => 1,
                        Key.Digit3 => 2,
                        Key.Digit4 => 3,
                        Key.Digit5 => 4,
                        Key.Digit6 => 5,
                        Key.Digit7 => 6,
                        Key.Digit8 => 7,
                        Key.Digit9 => 8,
                        Key.Digit0 => 9,
                        _ => -1
                    };
                    OnPerformedHotbar?.Invoke(index);
                }
            #endregion
        #endregion
        
        #region Mouse
            public static event Action OnClickedMouseLeftButton;
            private static void OnMouseLeftButtonClicked(InputAction.CallbackContext context) => OnClickedMouseLeftButton?.Invoke();
            public static void GetMousePosition(out Vector2 position) => position = InputManager.Mouse.MousePosition.ReadValue<Vector2>();
        #endregion
    }
}