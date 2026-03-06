using System;
using Input_System;
using Player_System.Object.State_Machine;
using Player_System.Object.State_Machine.State;
using Player_System.System.Player_System;
using UnityEngine;

namespace Player_System.Object
{
    public partial class PlayerObject : MonoBehaviour, IAttackable
    {
        [field: Header("Player System")]
        [field: SerializeField] private PlayerSystem playerSystem;
        
        private readonly StateMachine _stateMachine = new();

        private IState _idleState;
        private IState _moveState;
        private IState _hurtState;
        
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

            _moveState = new OnWalk(
                onEnter: () =>
                {
                    PlayWalkAnimation();
                },
                onExit: () =>
                {
                });

            _hurtState = new OnHurt(
                onEnter: () =>
                {
                    Debug.Log("A");
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

            _stateMachine.InitializeState(_idleState);
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

        private void OnPerformInteract()
        {
            InteractWithObject();
        }

        public void TakeDamage(int damage)
        {
            _stateMachine.ChangeState(_hurtState);
        }
    }
}