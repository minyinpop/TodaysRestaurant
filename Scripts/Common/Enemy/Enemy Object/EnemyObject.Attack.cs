using System;
using Common.Detect_Area;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Attack Settings")]
        [field: SerializeField] private DetectArea attackDetectArea;
        [field: SerializeField] private string attackDetectTag;

        private Action _onEnterAttackDetectCleanupAction;
        private Action _onExitAttackDetectCleanupAction;

        private void OnObjectEnterAttackDetectArea(GameObject obj)
        {
            if (obj.CompareTag(attackDetectTag))
            {
            }
        }
        
        private void OnObjectExitAttackDetectArea(GameObject obj)
        {
            if (obj.CompareTag(attackDetectTag))
            {
            }
        }
    }
}