using System;
using System.Collections.Generic;
using UnityEngine;

namespace Input_System
{
    public partial class InputSystem : MonoBehaviour
    {
        private static InputManager _inputManager;
        private readonly Queue<Action> _cleanupActions = new();

        private void Awake()
        {
            _inputManager = new InputManager();
        }

        private void Start()
        {
            _inputManager.Enable();
            
            _inputManager.Player.Walk.started += OnPlayerWalkStarted;
            _cleanupActions.Enqueue(() => _inputManager.Player.Walk.started -= OnPlayerWalkStarted);
                
            _inputManager.Player.Walk.canceled += OnPlayerWalkCanceled;
            _cleanupActions.Enqueue(() => _inputManager.Player.Walk.canceled -= OnPlayerWalkCanceled);
                
            _inputManager.Player.Hotbar.performed += OnHotbarPerformed;
            _cleanupActions.Enqueue(() => _inputManager.Player.Hotbar.performed -= OnHotbarPerformed);

            _inputManager.Player.Backpack.performed += OnBackpackPerformed;
            _cleanupActions.Enqueue(() => _inputManager.Player.Backpack.performed -= OnBackpackPerformed);
            
            _inputManager.Player.Map.performed += OnMapPerformed;
            _cleanupActions.Enqueue(() => _inputManager.Player.Map.performed -= OnMapPerformed);
            
            _inputManager.Mouse.LeftButton.performed += OnLeftButtonClicked;
            _cleanupActions.Enqueue(() => _inputManager.Mouse.LeftButton.performed -= OnLeftButtonClicked);
                
            _inputManager.Mouse.RightButtom.performed += OnRightButtonClicked;
            _cleanupActions.Enqueue(() => _inputManager.Mouse.RightButtom.performed -= OnRightButtonClicked);
        }
        
        private void OnDestroy()
        {
            while (_cleanupActions.Count > 0)
            {
                _cleanupActions.Dequeue()?.Invoke();
            }

            _inputManager.Disable();
        }
        
        public static void Enable()
        {
            _inputManager.Enable();
        }

        public static void Disable()
        {
            _inputManager.Disable();
        }
    }
}