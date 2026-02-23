using Common.Detect_Area;
using UnityEngine;

namespace Common.Enemy.Object
{
    public partial class EnemyObject
    {
        [field: Header("Detect Settings")]
        [field: SerializeField] private DetectArea detectArea;
    }
}