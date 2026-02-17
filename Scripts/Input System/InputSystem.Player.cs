using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Input_System
{
    public partial class InputSystem
    {
        #region Walk
            public static Vector2 WalkDirection => _inputManager.Player.Walk.ReadValue<Vector2>();
            
            public static event Action OnStartedPlayerWalk;
            private static void OnPlayerWalkStarted(InputAction.CallbackContext context)
            {
                OnStartedPlayerWalk?.Invoke();
            }
            
            public static event Action OnCancelPlayerWalk;
            private static void OnPlayerWalkCanceled(InputAction.CallbackContext context)
            {
                OnCancelPlayerWalk?.Invoke();
            }
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
            private static void OnBackpackPerformed(InputAction.CallbackContext context)
            {
                OnPerformedBackpack?.Invoke();
            }
        #endregion
        
        #region Map
            public static event Action OnPerformedMap;
            private void OnMapPerformed(InputAction.CallbackContext context)
            {
                OnPerformedMap?.Invoke();
            }
        #endregion
    }
}
