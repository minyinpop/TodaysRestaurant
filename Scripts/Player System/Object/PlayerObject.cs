using System;
using Common;
using Common.Item.Data;
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

        private void Awake()
        {
            #region PlayerSystem
                if (playerSystem is null)
                {
                    throw new InvalidOperationException($"{gameObject.name} > {GetType().Name} > {nameof(playerSystem)} cannot be null.");
                }
            #endregion
            
            #region PlayerSystem.Interact
                if (detectArea is null)
                {
                    throw new InvalidOperationException($"{gameObject.name} > {GetType().Name} > {nameof(detectArea)} cannot be null.");
                }
            #endregion
            
            #region PlayerSystem.Move
                if (rig is null)
                {
                    throw new InvalidOperationException($"{gameObject.name} > {GetType().Name} > {nameof(rig)} cannot be null.");
                }
                
                if (playerCharacterData is null)
                {
                    throw new InvalidOperationException($"{gameObject.name} > {GetType().Name} > {nameof(playerCharacterData)} cannot be null.");
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
                    // TODO 被攻擊到的動畫
                    _canInteract = false;
                    _canWalk = false;
                },
                onExit: () =>
                {
                });
            
            InputSystem.OnPerformedInteract += OnPerformInteract;
        }

        private void Start()
        {
            StartDetectInteractableObject();
            _stateMachine.InitializeState(_idleState);
        }

        private void FixedUpdate()
        {
            DetectMove();
        }

        private void OnDestroy()
        {
            StopDetectInteractableObject();
            InputSystem.OnPerformedInteract -= OnPerformInteract;
        }

        private void OnPerformInteract()
        {
            InteractWithObject();
        }

        public bool TakeDamage()
        {
            _stateMachine.ChangeState(_hurtState);
            return true;
        }

        public bool TryAddItem(IItem itemData)
        {
            return playerSystem.TryAddItem(itemData);
        }
    }
}