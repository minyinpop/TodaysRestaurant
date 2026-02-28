using System;
using Common.Detect_Area;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Detect Settings")]
        [field: SerializeField] private DetectArea chaseDetectArea;
        [field: SerializeField] private string chaseDetectTag;
        
        private Action _onEnterChaseDetectCleanupAction;
        private Action _onExitChaseDetectCleanupAction;

        private GameObject _chasingTarget;

        private void OnObjectEnterChaseDetectArea(GameObject obj)
        {
            if (obj.CompareTag(chaseDetectTag))
            {
                _chasingTarget = obj;
                
                StartChaseState();
            }
        }

        private void OnObjectExitChaseDetectArea(GameObject obj)
        {
            if (obj.CompareTag(chaseDetectTag))
            {
                _chasingTarget = null;
                
                StopChaseState();
            }
        }
    }
}