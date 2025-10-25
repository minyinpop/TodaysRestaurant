using System.Collections.Generic;
using System.Cook.Child.Customer.Object.Child;
using System.Cook.Child.Customer.Object.Main.State_Machine;
using System.Cook.Child.Customer.Object.Main.State_Machine.State;
using System.Cook.Child.Queue.Object;
using UnityEngine;

namespace System.Cook.Child.Customer.Object.Main
{
    internal sealed class Customer : MonoBehaviour
    {
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private FlipSystem FlipSystem;
        [field: SerializeField] private SkinSystem SkinSystem;

        private QueuePoint QueuePoint;
        
        private readonly StateMachine StateMachine = new();
        
        private readonly List<Action> ActiveActions = new();

        private void OnEnable()
        {
            MoveSystem.WalkLeft += FlipSystem.TurnsLeft;
            ActiveActions.Add(() => MoveSystem.WalkLeft -= FlipSystem.TurnsLeft);
                
            MoveSystem.WalkRight += FlipSystem.TurnsRight;
            ActiveActions.Add(() => MoveSystem.WalkRight -= FlipSystem.TurnsRight);
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        public void SetSkin() => SkinSystem.SetRandomSkin();

        public void WalkToQueuePoint(QueuePoint point) { QueuePoint = point; OnWalkToQueuePoint(); }

        private void OnWalkToQueuePoint()
        {
            StateMachine.ChangeState(new WalkToQueuePoint(OnEnter, OnExit));
            return;

            void OnEnter()
            {
                AnimationSystem.Walk();

                QueuePoint.GetStandPoint(out var standPoint);
                MoveSystem.StartWalk(standPoint, OnArrive);
                return;

                void OnArrive()
                {
                    AnimationSystem.Idle();
                    MoveSystem.StopWalk();
                    // TODO 向 CookSystem Func Seat 的資料回來，然後切到 OnWalkToSeat，並且後面排隊的顧客也要往前走
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
            }
            
            void OnExit()
            {
            }
        }
    }
}