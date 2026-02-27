using Unity.AI.Navigation;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Move Settings")]
        [field: SerializeField] private NavMeshSurface navMesh;
    }
}