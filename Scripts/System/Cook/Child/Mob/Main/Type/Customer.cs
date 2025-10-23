using System.Mob.Child;
using System.Mob.Main.Base;
using System.Mob.Main.Base.State_Machine;
using UnityEngine;

namespace System.Mob.Main.Type
{
    internal sealed class Customer : MobSystem
    {
        [field: Header("Child System")]
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        private readonly StateMachine StateMachine = new();
    }
}