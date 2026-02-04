using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input_System
{
    public sealed class LobbyInputSystem : MonoBehaviour
    {
        // Base
        private static InputManager _inputManager;
        private readonly Queue<Action> _cleanupActions = new();
        
        // Interface
        public static event Action OnLobbyLevelSelectUIPerformedAction;

        private void Awake()
        {
            _inputManager = new InputManager();
        }

        private void Start()
        {
            _inputManager.Enable();
            
            _inputManager.Lobby.LevelSelectUI.performed += OnLobbyLevelSelectUIPerformed;
            _cleanupActions.Enqueue(() => _inputManager.Lobby.LevelSelectUI.performed -= OnLobbyLevelSelectUIPerformed);
        }

        private void OnDestroy()
        {
            while (_cleanupActions.Count > 0)
            {
                _cleanupActions.Dequeue()?.Invoke();
            }

            _inputManager.Disable();
        }
        
        private void OnLobbyLevelSelectUIPerformed(InputAction.CallbackContext context)
        {
            OnLobbyLevelSelectUIPerformedAction?.Invoke();
        }
    }
}