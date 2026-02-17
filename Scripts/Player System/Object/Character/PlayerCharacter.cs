using System;
using System.Collections.Generic;
using Input_System;
using Player_System.Object.Character.State_Machine;
using Player_System.Object.Character.State_Machine.State;
using UnityEngine;

namespace Player_System.Object.Character
{
    public partial class PlayerCharacter : MonoBehaviour
    {
        private readonly StateMachine _stateMachine = new();
        
        private readonly Queue<Action> _cleanUpActions = new();
        
        private void Start()
        {
            InputSystem.OnCancelPlayerWalk += OnIdle;
            _cleanUpActions.Enqueue(() => InputSystem.OnCancelPlayerWalk -= OnIdle);
                
            WalkLeft += TurnsLeft;
            _cleanUpActions.Enqueue(() => WalkLeft -= TurnsLeft);
                            
            WalkRight += TurnsRight;
            _cleanUpActions.Enqueue(() => WalkRight -= TurnsRight);
            
            StartDetect();
        }
        
        private void OnDestroy()
        {
            StopDetect();
            
            WalkLeft -= TurnsLeft;
            WalkRight -= TurnsRight;
            
            while (_cleanUpActions.Count > 0)
            {
                _cleanUpActions.Dequeue()?.Invoke();
            }
        }
        
        #region StateMachine
            #region OnIdle
                private Action OnStartedPlayerWalkCleanupAction;
            
                private void OnIdle()
                {
                    _stateMachine.ChangeState(new OnIdle(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        InputSystem.OnStartedPlayerWalk += OnWalk;
                        OnStartedPlayerWalkCleanupAction = () =>
                        {
                            InputSystem.OnStartedPlayerWalk -= OnWalk;
                        };
                        
                        Idle();
                    }
                    
                    void OnExit()
                    {
                        OnStartedPlayerWalkCleanupAction?.Invoke();
                        OnStartedPlayerWalkCleanupAction = null;
                    }
                }
            #endregion

            #region OnWalk
                private Action OnCancelPlayerWalkCleanupAction;
                
                private void OnWalk()
                {
                    _stateMachine.ChangeState(new OnWalk(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        StartWalk();
                        Walk();
                    }
                    
                    void OnExit()
                    {
                        StopWalk();
                    }
                }
            #endregion
        #endregion
    }
}