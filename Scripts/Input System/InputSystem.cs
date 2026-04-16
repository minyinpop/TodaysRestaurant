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
            
            _inputManager.Enable();
            
            #region Player   
                _inputManager.Player.Hotbar.performed += OnHotbarPerformed;
                _cleanupActions.Enqueue(() => _inputManager.Player.Hotbar.performed -= OnHotbarPerformed);
                
                _inputManager.Player.Interact.performed += OnInteractPerformed;
                _cleanupActions.Enqueue(() => _inputManager.Player.Interact.performed -= OnInteractPerformed);

                _inputManager.Player.Tab.performed += OnTabPerformed;
                _cleanupActions.Enqueue(() => _inputManager.Player.Tab.performed -= OnTabPerformed);
            #endregion
            
            #region Mouse
                _inputManager.Mouse.LeftButton.performed += OnLeftButtonClicked;
                _cleanupActions.Enqueue(() => _inputManager.Mouse.LeftButton.performed -= OnLeftButtonClicked);
                    
                _inputManager.Mouse.RightButtom.performed += OnRightButtonClicked;
                _cleanupActions.Enqueue(() => _inputManager.Mouse.RightButtom.performed -= OnRightButtonClicked);
            #endregion
        }
        
        private void OnDestroy()
        {
            while (_cleanupActions.Count > 0)
            {
                _cleanupActions.Dequeue()?.Invoke();
            }

            _inputManager.Disable();
        }
    }
}