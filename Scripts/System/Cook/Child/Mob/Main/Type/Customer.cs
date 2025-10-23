using System.Cook.Child.Mob.Child;
using System.Cook.Child.Mob.Main.Base;
using System.Cook.Child.Mob.Main.Base.State_Machine;
using System.Cook.Child.Mob.Main.Base.State_Machine.State;
using UnityEngine;

namespace System.Cook.Child.Mob.Main.Type
{
    internal sealed class Customer : MobSystem
    {
        [field: Header("Child System")]
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        private readonly StateMachine StateMachine = new();

        private void Start()
        {
            OnSearchingForSeat();
        }

        private void OnSearchingForSeat()
        {
            StateMachine.ChangeState(new OnSearchingForSeat(OnEnter, OnExit));
            return;

            void OnEnter()
            {
                MoveSystem.StartWalk();
                AnimationSystem.Walk();
            }
            
            void OnExit()
            {
            }
        }
    }
}