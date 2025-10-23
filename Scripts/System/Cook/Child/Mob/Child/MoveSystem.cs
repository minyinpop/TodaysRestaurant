using Data.Mob.Character.Main;
using UnityEngine;
using UnityEngine.AI;

namespace System.Cook.Child.Mob.Child
{
    internal sealed class MoveSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Rigidbody Rig;
        [field: SerializeField] private NavMeshAgent Agent;
        [field: SerializeField] private Transform Target;
        
        [field: Header("Data")]
        [field: SerializeField] private CharacterSO CharacterData;

        public void StartWalk()
        {
            Agent.SetDestination(Target.position);
        }
    }
}