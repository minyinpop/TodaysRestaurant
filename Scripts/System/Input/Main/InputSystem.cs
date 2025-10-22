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
            
            #region Mouse
                InputManager.Mouse.LeftButton.started += OnMouseLeftButtonClicked;
            #endregion
            
            #region Player
                InputManager.Player.Walk.started += OnPlayerWalkStarted;
                InputManager.Player.Walk.canceled += OnPlayerWalkCanceled;
                InputManager.Player.WalkLeft.started += OnPlayerWalkLeftStarted;
                InputManager.Player.WalkRight.started += OnPlayerWalkRightStarted;
            #endregion
        }

        private void OnDisable()
        {
            #region Mouse
                InputManager.Mouse.LeftButton.started -= OnMouseLeftButtonClicked;
            #endregion
            
            #region Player
                InputManager.Player.Walk.started -= OnPlayerWalkStarted;
                InputManager.Player.Walk.canceled -= OnPlayerWalkCanceled;
                InputManager.Player.WalkLeft.started -= OnPlayerWalkLeftStarted;
                InputManager.Player.WalkRight.started -= OnPlayerWalkRightStarted;
            #endregion
            
            InputManager.Disable();
        }
        
        #region Mouse
            public static event Action OnClickMouseLeftButton;
            private void OnMouseLeftButtonClicked(InputAction.CallbackContext context) { OnClickMouseLeftButton?.Invoke(); }
            
            public static void GetMousePosition(out Vector2 position) { position = InputManager.Mouse.MousePosition.ReadValue<Vector2>(); }
        #endregion
        
        #region Player
            #region State
                public static event Action OnStartedPlayerWalk;
                private static void OnPlayerWalkStarted(InputAction.CallbackContext context) => OnStartedPlayerWalk?.Invoke();

                public static event Action OnCancelPlayerWalk;
                private static void OnPlayerWalkCanceled(InputAction.CallbackContext context) => OnCancelPlayerWalk?.Invoke();
                
                public static event Action OnStartedPlayerWalkLeft;
                private static void OnPlayerWalkLeftStarted(InputAction.CallbackContext context) => OnStartedPlayerWalkLeft?.Invoke();
                
                public static event Action OnStartedPlayerWalkRight;
                private static void OnPlayerWalkRightStarted(InputAction.CallbackContext context) => OnStartedPlayerWalkRight?.Invoke();
            #endregion
            
            #region Value
                public static void GetPlayerWalkDirection(out Vector2 direction) => direction = InputManager.Player.Walk.ReadValue<Vector2>();
            #endregion
        #endregion
    }
}