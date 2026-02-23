using System;
using Common.Detect_Area;
using UnityEngine;

namespace Common.Enemy.Object
{
    public partial class EnemyObject
    {
        [field: Header("Detect Settings")]
        [field: SerializeField] private DetectArea detectArea;
        
        private Action _onEnterDetectCleanupAction;
        private Action _onExitDetectCleanupAction;

        private void OnObjectEnterDetect(GameObject obj)
        {
            Debug.Log("Enter");
        }
        
        private void OnObjectExitDetect(GameObject obj)
        {
            Debug.Log("Exit");
        }
    }
}