using System.Collections.Generic;
using System.Economy.Child.Customer.Child;
using System.Economy.Child.Customer.Main.State_Machine;
using System.Economy.Child.Customer.Main.State_Machine.State;
using UnityEngine;

namespace System.Economy.Child.Customer.Main
{
    internal sealed class CustomerSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private FlipSystem FlipSystem;
        [field: SerializeField] private SkinSystem SkinSystem;
        
        private readonly StateMachine StateMachine = new();

        private readonly List<Action> ActiveActions = new();
        
        private void OnEnable()
        {
            MoveSystem.ToLeft += FlipSystem.TurnsLeft;
            ActiveActions.Add(() => MoveSystem.ToLeft -= FlipSystem.TurnsLeft);
                
            MoveSystem.ToRight += FlipSystem.TurnsRight;
            ActiveActions.Add(() => MoveSystem.ToRight -= FlipSystem.TurnsRight);
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }
        
        #region StateMachine
            #region WalkToQueuePoint
                public void WalkToQueuePoint(Transform standPoint)
                {
                    StateMachine.ChangeState(new WalkToQueuePoint(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        SkinSystem.SetRandomSkin();
                        AnimationSystem.Walk();
                        MoveSystem.StartWalk(standPoint,
                            onArrive: () =>
                            {
                                AnimationSystem.Idle();
                                MoveSystem.StopWalk();
                            });
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion
            
            #region WalkToSeatPoint
                public void WalkToSeatPoint(Transform standPoint, Transform sitPoint, Action onArrive)
                {
                    StateMachine.ChangeState(new WalkToSeatPoint(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        AnimationSystem.Walk();
                        MoveSystem.StartWalk(standPoint,
                            onArrive: () =>
                            {
                                AnimationSystem.Sit();
                                MoveSystem.StopWalk();
                                onArrive?.Invoke();
                                SitOnSeatPoint(sitPoint);
                            });
                    }
                        
                    void OnExit()
                    {
                    }
                }
            #endregion
            
            #region SitOnSeatPoint
                private void SitOnSeatPoint(Transform sitPoint)
                {
                    AnimationSystem.Sit();
                    MoveSystem.SitDown(sitPoint);
                }
            #endregion
        #endregion
    }
}