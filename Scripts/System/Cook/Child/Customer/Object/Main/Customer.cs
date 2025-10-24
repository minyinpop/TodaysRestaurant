using System.Collections.Generic;
using System.Cook.Child.Customer.Object.Child;
using System.Cook.Child.Customer.Object.Main.State_Machine;
using System.Cook.Child.Customer.Object.Main.State_Machine.State;
using System.Cook.Child.Seating.Object;
using UnityEngine;

namespace System.Cook.Child.Customer.Object.Main
{
    internal sealed class Customer : MonoBehaviour
    {
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private FlipSystem FlipSystem;
        [field: SerializeField] private SkinSystem SkinSystem;

        private GameObject QueuePoint;
        private GameObject Seat;
        
        private readonly StateMachine StateMachine = new();
        
        private readonly List<Action> ActiveActions = new();

        public event Func<Seat> FindSeat;

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        public void Init()
        {
            SkinSystem.SetRandomSkin();
            OnWalkToDoor();
        }

        private void OnWalkToDoor()
        {
            StateMachine.ChangeState(new WalkToDoor(OnEnter, OnExit));
            return;

            void OnEnter()
            {
                AnimationSystem.Walk();
                // MoveSystem.StartWalk(, OnArrive);
                
                MoveSystem.WalkLeft += FlipSystem.TurnsLeft;
                ActiveActions.Add(() => MoveSystem.WalkLeft -= FlipSystem.TurnsLeft);
                
                MoveSystem.WalkRight += FlipSystem.TurnsRight;
                ActiveActions.Add(() => MoveSystem.WalkRight -= FlipSystem.TurnsRight);
                return;

                void OnArrive()
                {
                    Debug.Log("Arrived.");
                    AnimationSystem.Idle();
                    MoveSystem.StopWalk();
                }
            }
            
            void OnExit()
            {
            }
        }

        private void OnWalkToSeat()
        {
            StateMachine.ChangeState(new WalkToSeat(OnEnter, OnExit));
            return;

            void OnEnter()
            {
                AnimationSystem.Walk();
                MoveSystem.StartWalk(Seat, OnArrive);
                
                MoveSystem.WalkLeft += FlipSystem.TurnsLeft;
                ActiveActions.Add(() => MoveSystem.WalkLeft -= FlipSystem.TurnsLeft);
                
                MoveSystem.WalkRight += FlipSystem.TurnsRight;
                ActiveActions.Add(() => MoveSystem.WalkRight -= FlipSystem.TurnsRight);
                return;

                void OnArrive()
                {
                    Debug.Log("Arrived.");
                    AnimationSystem.Idle();
                    MoveSystem.StopWalk();
                }
            }
            
            void OnExit()
            {
            }
        }
    }
}