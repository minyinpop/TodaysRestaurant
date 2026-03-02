using System;
using System.Collections.Generic;
using System.Linq;
using Common.Detect_Area;
using Common.Interactable_Object;
using UnityEngine;

namespace Player_System.Object
{
    public partial class PlayerObject
    {
        [field: Header("Detect System")]
        [field: SerializeField] private DetectArea detectArea;

        private Action _onEnterDetectCleanupAction;
        private Action _onExitDetectCleanupAction;
        
        private readonly List<InteractableObject> _interactableObjects = new();

        private void StartDetectInteractableObject()
        {
            detectArea.OnEnterDetect += OnEnterDetect;
            detectArea.OnExitDetect += OnExitDetect;
            _onEnterDetectCleanupAction = () => detectArea.OnEnterDetect -= OnEnterDetect;
            _onExitDetectCleanupAction = () => detectArea.OnExitDetect -= OnExitDetect;
            return;

            void OnEnterDetect(GameObject detectObj)
            {
                if (!detectObj.TryGetComponent<InteractableObject>(out var detectObjectScript)) return;
                if (_interactableObjects.Contains(detectObjectScript)) return;
                
                _interactableObjects.Add(detectObjectScript);
                detectObjectScript.OnEnterDetect();
            }

            void OnExitDetect(GameObject detectObj)
            {
                if (!detectObj.TryGetComponent<InteractableObject>(out var detectObjectScript)) return;
                
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
            if (_interactableObjects.Count == 0)
            {
                return;
            }

            if (_interactableObjects.First() == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(_interactableObjects)} the first object data is be null.");
                return;
            }
            
            var interactableObject = _interactableObjects.First();

            if (interactableObject.OnInteract(playerSystem))
            {
                _interactableObjects.Remove(interactableObject);
            }
        }
    }
}