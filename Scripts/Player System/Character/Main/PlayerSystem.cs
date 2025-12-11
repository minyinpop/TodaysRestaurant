using System;
using System.Collections.Generic;
using Input_System.Main;
using Player_System.Character.Child;
using Player_System.Character.Child.Detect_System.Main;
using Player_System.Character.Main.State_Machine;
using Player_System.Character.Main.State_Machine.State;
using UnityEngine;

namespace Player_System.Character.Main
{
    internal sealed class PlayerSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private DetectSystem DetectSystem;

        private readonly StateMachine StateMachine = new();
        
        private readonly List<Action> ActiveActions = new();

        private void Start()
        {
            OnIdle();
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        #region StateMachine
            private void OnIdle()
            {
                StateMachine.ChangeState(new OnIdle(OnEnter, OnExit));
                return;

                void OnEnter()
                {
                    InputSystem.OnStartedPlayerWalk += OnWalk;
                    ActiveActions.Add(() => InputSystem.OnStartedPlayerWalk -= OnWalk);
                    
                    AnimationSystem.Idle();
                }
                
                void OnExit()
                {
                    InputSystem.OnStartedPlayerWalk -= OnWalk;
                }
            }

            private void OnWalk()
            {
                StateMachine.ChangeState(new OnWalk(OnEnter, OnExit));
                return;

                void OnEnter()
                {
                    InputSystem.OnCancelPlayerWalk += OnIdle;
                    ActiveActions.Add(() => InputSystem.OnCancelPlayerWalk -= OnIdle);

                    MoveSystem.WalkLeft += AnimationSystem.TurnsLeft;
                    ActiveActions.Add(() => MoveSystem.WalkLeft -= AnimationSystem.TurnsLeft);
                    
                    MoveSystem.WalkRight += AnimationSystem.TurnsRight;
                    ActiveActions.Add(() => MoveSystem.WalkRight -= AnimationSystem.TurnsRight);

                    MoveSystem.StartWalk();
                    AnimationSystem.Walk();
                }
                
                void OnExit()
                {
                    InputSystem.OnCancelPlayerWalk -= OnIdle;
                    MoveSystem.WalkLeft -= AnimationSystem.TurnsLeft;
                    MoveSystem.WalkRight -= AnimationSystem.TurnsRight;
                    MoveSystem.StopWalk();
                }
            }
        #endregion
    }
}