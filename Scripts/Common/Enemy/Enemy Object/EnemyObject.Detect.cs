using System;
using Common.Detect_Area;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Detect Settings")]
        [field: SerializeField] private DetectArea detectArea;
        [field: SerializeField] private string detectTag;
        
        private Action _onEnterDetectCleanupAction;
        private Action _onExitDetectCleanupAction;

        private GameObject _chasingTarget;

        private void OnObjectEnterDetect(GameObject obj)
        {
            if (obj.CompareTag(detectTag))
            {
                _chasingTarget = obj;
                
                AlertState();
            }
        }
        
        private void OnObjectExitDetect(GameObject obj)
        {
            if (obj.CompareTag(detectTag))
            {
                _chasingTarget = null;
                
                IdleState();
            }
        }
    }
}