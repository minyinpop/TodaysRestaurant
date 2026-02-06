using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Input_System.Main
{
    public sealed class InputSystem : MonoBehaviour
    {
        private static InputManager InputManager;
        
        private readonly Queue<Action> ActiveActions = new();

        private void Awake()
        {
            InputManager = new InputManager();
        }

        private void OnEnable()
        {
            Enable();
            
            #region Player
                InputManager.Player.Walk.started += OnPlayerWalkStarted;
                ActiveActions.Enqueue(() => InputManager.Player.Walk.started -= OnPlayerWalkStarted);
                
                InputManager.Player.Walk.canceled += OnPlayerWalkCanceled;
                ActiveActions.Enqueue(() => InputManager.Player.Walk.canceled -= OnPlayerWalkCanceled);
                
                InputManager.Player.Hotbar.performed += OnHotbarPerformed;
                ActiveActions.Enqueue(() => InputManager.Player.Hotbar.performed -= OnHotbarPerformed);

                InputManager.Player.Backpack.performed += OnBackpackPerformed;
                ActiveActions.Enqueue(() => InputManager.Player.Backpack.performed -= OnBackpackPerformed);
            #endregion
            
            #region Mouse
                InputManager.Mouse.LeftButton.performed += OnLeftButtonClicked;
                ActiveActions.Enqueue(() => InputManager.Mouse.LeftButton.performed -= OnLeftButtonClicked);
                
                InputManager.Mouse.RightButtom.performed += OnRightButtonClicked;
                ActiveActions.Enqueue(() => InputManager.Mouse.RightButtom.performed -= OnRightButtonClicked);
            #endregion
        }

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
            Disable();
        }

        #region // TODO Rename 主控
            public static void Enable()
            {
                InputManager.Enable();
            }

            public static void Disable()
            {
                InputManager.Disable();
            }
        #endregion
        
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

            #region Backpack
                public static event Action OnPerformedBackpack;
                private static void OnBackpackPerformed(InputAction.CallbackContext context) => OnPerformedBackpack?.Invoke();
            #endregion
        #endregion
        
        #region Mouse
            public static event Action OnClickedLeftButton;
            private static void OnLeftButtonClicked(InputAction.CallbackContext context) => OnClickedLeftButton?.Invoke();
            
            public static event Action OnClickedRightButton;
            private static void OnRightButtonClicked(InputAction.CallbackContext context) => OnClickedRightButton?.Invoke();
            
            public static Vector2 MousePosition() => InputManager.Mouse.MousePosition.ReadValue<Vector2>();
        #endregion
    }
}