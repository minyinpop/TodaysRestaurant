using System;
using Input_System;
using Player_System.Object.State_Machine;
using Player_System.Object.State_Machine.State;
using Player_System.System.Player_System;
using UnityEngine;

namespace Player_System.Object
{
    public partial class PlayerObject : MonoBehaviour
    {
        [field: Header("Player System")]
        [field: SerializeField] private PlayerSystem playerSystem;
        
        private readonly StateMachine _stateMachine = new();

        private IState _idleState;
        private IState _walkState;
        
        private Action OnPerformInteractCleanupAction;

        private void Awake()
        {
            #region PlayerSystem
                if (playerSystem == null)
                {
                    Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(playerSystem)} cannot be null.");
                    gameObject.SetActive(false);
                    return;
                }
            #endregion
            
            #region PlayerSystem.Interact
                if (detectArea == null)
                {
                    Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(detectArea)} cannot be null.");
                    gameObject.SetActive(false);
                    return;
                }
            #endregion
            
            #region PlayerSystem.Move
                if (rig == null)
                {
                    Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(rig)} cannot be null.");
                    gameObject.SetActive(false);
                    return;
                }
                
                if (attributeSO == null)
                {
                    Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(attributeSO)} cannot be null.");
                    gameObject.SetActive(false);
                    return;
                }
            #endregion

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