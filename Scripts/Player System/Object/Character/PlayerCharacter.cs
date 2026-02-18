using System;
using Input_System;
using Player_System.Object.Character.State_Machine;
using Player_System.Object.Character.State_Machine.State;
using UnityEngine;

namespace Player_System.Object.Character
{
    public partial class PlayerCharacter : MonoBehaviour
    {
        private readonly StateMachine _stateMachine = new();

        private IState _idleState;
        private IState _walkState;
        
        private Action OnPerformInteractCleanupAction;

        private void Awake()
        {
            _idleState = new OnIdle(
                onEnter: () =>
                {
                    PlayIdleAnimation();
                },
                onExit: () =>
                {
                });

            _walkState = new OnWalk(
                onEnter: () =>
                {
                    PlayWalkAnimation();
                },
                onExit: () =>
                {
                });
        }

        private void Start()
        {
            InputSystem.OnPerformedInteract += OnPerformInteract;
            OnPerformInteractCleanupAction = () => InputSystem.OnPerformedInteract -= OnPerformInteract;
            
            StartDetectInteractableObject();

            InitializeState();
        }

        private void FixedUpdate()
        {
            DetectMove();
        }

        private void Update()
        {
            DetectFlip();
        }

        private void OnDestroy()
        {
            StopDetectInteractableObject();
            
            OnPerformInteractCleanupAction?.Invoke();
        }
        
        #region StateMachine
            private void InitializeState()
            {
                IdleState();
            }
            
            private void IdleState()
            {
                _stateMachine.ChangeState(_idleState);
            }
            
            private void WalkState()
            {
                _stateMachine.ChangeState(_walkState);
            }
        #endregion

        private void OnPerformInteract()
        {
            InteractWithObject();
        }
    }
}