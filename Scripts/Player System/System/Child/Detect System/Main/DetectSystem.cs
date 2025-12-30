using System;
using System.Collections.Generic;
using Common;
using Player_System.System.Child.Detect_System.Child;
using UnityEngine;

namespace Player_System.System.Child.Detect_System.Main
{
    public sealed class DetectSystem : MonoBehaviour
    {
        [field: Header("Detect Area")]
        [field: SerializeField] private DetectArea[] DetectAreas;
        
        private readonly Queue<Action> _activeActions = new();
        
        private void Start()
        {
            StartDetect();
        }

        private void OnDisable()
        {
            StopDetect();
        }

        private void StartDetect()
        {
            foreach (var detectArea in DetectAreas)
            {
                detectArea.OnEnterDetect += OnEnterDetect;
                detectArea.OnExitDetect += OnExitDetect;
                _activeActions.Enqueue(() => detectArea.OnEnterDetect -= OnEnterDetect);
                _activeActions.Enqueue(() => detectArea.OnExitDetect -= OnExitDetect);
            }
            return;

            void OnEnterDetect(GameObject detectedObject)
            {
                detectedObject.GetComponent<InteractableObject>().OnEnterDetect();
            }

            void OnExitDetect(GameObject detectedObject)
            {
                detectedObject.GetComponent<InteractableObject>().OnExitDetect();
            }
        }

        private void StopDetect()
        {
            while (_activeActions.Count > 0)
            {
                var action = _activeActions.Dequeue();
                action?.Invoke();
            }
        }
    }
}