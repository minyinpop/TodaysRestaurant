using System;
using System.Collections.Generic;
using Common.Detect_Area;
using Common.Interactable_Object;
using UnityEngine;

namespace Player_System.Object
{
    public partial class PlayerObject
    {
        [field: Header("偵測系統 - 範圍")]
        [field: SerializeField] private DetectArea detectArea;

        private Action _onEnterDetectCleanupAction;
        private Action _onExitDetectCleanupAction;
        
        private readonly List<InteractableObject> _interactableObjects = new();
        private InteractableObject _currentInteractObject;

        private bool _canInteract = true;

        private void StartDetectInteractableObject()
        {
            detectArea.OnEnterDetect += OnEnterDetect;
            detectArea.OnExitDetect += OnExitDetect;
            _onEnterDetectCleanupAction = () => detectArea.OnEnterDetect -= OnEnterDetect;
            _onExitDetectCleanupAction = () => detectArea.OnExitDetect -= OnExitDetect;
            return;

            void OnEnterDetect(GameObject detectObj)
            {
                if (!detectObj.TryGetComponent<InteractableObject>(out var detectObjectScript))
                {
                    return;
                }
                
                if (_interactableObjects.Contains(detectObjectScript))
                {
                    return;
                }
                
                _interactableObjects.Add(detectObjectScript);
                detectObjectScript.OnEnterDetect(this);
            }

            void OnExitDetect(GameObject detectObj)
            {
                if (!detectObj.TryGetComponent<InteractableObject>(out var detectObjectScript))
                {
                    return;
                }
                
                _interactableObjects.Remove(detectObjectScript);
                detectObjectScript.OnExitDetect();
            }
        }

        private void StopDetectInteractableObject()
        {
            _onEnterDetectCleanupAction.Invoke();
            _onExitDetectCleanupAction.Invoke();
        }

        private void InteractWithObject()
        {
            if (!_canInteract)
            {
                return;
            }
            
            if (_interactableObjects.Count <= 0)
            {
                return;
            }

            for (var i = 0; i < _interactableObjects.Count; i++)
            {
                if (_interactableObjects[i] is null)
                {
                    _interactableObjects.RemoveAt(i);
                    continue;
                }

                if (!_interactableObjects[i].Interactable)
                {
                    continue;
                }

                _currentInteractObject = _interactableObjects[i];
                
                _stateMachine.ChangeState(_takeItemState);
                return;
            }
        }

        private void InvokeInteract()
        {
            _currentInteractObject.Interact(this);
            _currentInteractObject = null;
        }
    }
}