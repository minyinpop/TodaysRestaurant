using Common.Detect_Area;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Detect Settings")]
        [field: SerializeField] private DetectArea chaseDetectArea;
        [field: SerializeField] private string chaseTargetTag;
        
        private GameObject _chaseTarget;
        private Vector3 _lastChaseTargetPosition;

        private void OnObjectEnterChaseDetectArea(GameObject obj)
        {
            if (obj.CompareTag(chaseTargetTag))
            {
                _chaseTarget = obj;
                _lastChaseTargetPosition = obj.transform.position;
            }
        }

        private void OnObjectExitChaseDetectArea(GameObject obj)
        {
            if (obj.CompareTag(chaseTargetTag))
            {
                _chaseTarget = null;
            }
        }
    }
}