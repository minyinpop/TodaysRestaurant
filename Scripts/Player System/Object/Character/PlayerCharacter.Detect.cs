using System;
using Common.Interactable_Object;
using UnityEngine;

namespace Player_System.Object.Character
{
    public partial class PlayerCharacter
    {
        [field: Header("Detect System")]
        [field: SerializeField] private DetectArea[] DetectAreas;

        private Action _onEnterDetectCleanupAction;
        private Action _onExitDetectCleanupAction;

        private void StartDetect()
        {
            foreach (var detectArea in DetectAreas)
            {
                detectArea.OnEnterDetect += OnEnterDetect;
                detectArea.OnExitDetect += OnExitDetect;
                _onEnterDetectCleanupAction = () => detectArea.OnEnterDetect -= OnEnterDetect;
                _onExitDetectCleanupAction = () => detectArea.OnExitDetect -= OnExitDetect;
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
            _onEnterDetectCleanupAction.Invoke();
            _onExitDetectCleanupAction.Invoke();
        }
    }
}