using System;
using System.Collections.Generic;
using Common;
using Player_System.Character.Child.Detect_System.Child;
using UnityEngine;

namespace Player_System.Character.Child.Detect_System.Main
{
    internal sealed class DetectSystem : MonoBehaviour
    {
        [field: Header("Detect Area")]
        [field: SerializeField] private DetectArea[] DetectAreas;
        
        private readonly Queue<Action> ActiveActions = new();
        
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
                ActiveActions.Enqueue(() => detectArea.OnEnterDetect -= OnEnterDetect);
                ActiveActions.Enqueue(() => detectArea.OnExitDetect -= OnExitDetect);
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
            while (ActiveActions.Count > 0)
            {
                var action = ActiveActions.Dequeue();
                action?.Invoke();
            }
        }
    }
}