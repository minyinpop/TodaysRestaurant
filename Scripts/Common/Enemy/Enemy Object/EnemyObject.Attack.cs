using Common.Detect_Area;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Attack Settings")]
        [field: SerializeField] private DetectArea attackDetectArea;
        [field: SerializeField] private string attackTargetTag;

        private GameObject _attackTarget;

        private void OnObjectEnterAttackDetectArea(GameObject obj)
        {
            if (obj.CompareTag(attackTargetTag))
            {
                _attackTarget = obj;
            }
        }
        
        private void OnObjectExitAttackDetectArea(GameObject obj)
        {
            if (obj.CompareTag(attackTargetTag))
            {
                _attackTarget = null;
            }
        }
    }
}