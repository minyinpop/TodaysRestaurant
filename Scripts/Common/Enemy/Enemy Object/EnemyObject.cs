using Common.Enemy.Data;
using Common.Enemy.Enemy_Object.State_Machine;
using Common.Enemy.Enemy_Object.State_Machine.State;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private Transform root;
                                public Transform Root => root;
        
        [field: Header("Data")]
        [field: SerializeField] private EnemySO enemyData;
        
        private readonly StateMachine _stateMachine = new();

        private IState _idleState;
        private IState _chaseState;
        private IState _attackState;
        
        private void Awake()
        {
            if (enemyData is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(enemyData)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            #region Animation
                if (animation is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }

                animation.AnimationState.Event += AnimationEvent;
            #endregion
            
            #region Attack
                if (attackDetectArea is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(attackDetectArea)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }

                attackDetectArea.OnEnterDetect += OnObjectEnterAttackDetectArea;
                attackDetectArea.OnExitDetect += OnObjectExitAttackDetectArea;
            #endregion

            #region Detect
                if (chaseDetectArea is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(chaseDetectArea)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
                
                chaseDetectArea.OnEnterDetect += OnObjectEnterChaseDetectArea;
                chaseDetectArea.OnExitDetect += OnObjectExitChaseDetectArea;
            #endregion

            _idleState = new OnIdle(
                onEnter: () =>
                {
                    PlayIdleAnimation();
                },
                onUpdate: () =>
                {
                    if (_chaseTarget is not null)
                    {
                        _stateMachine.ChangeState(_chaseState);
                    }
                },
                onExit: () =>
                {
                });

            _chaseState = new OnChase(
                onEnter: () =>
                {
                },
                onUpdate: () =>
                {
                    if (_isAnimationEnd)
                    {
                        if (_attackTarget is null)
                        {
                            if (_chaseTarget is null)
                            {
                                _stateMachine.ChangeState(_idleState);
                            }
                            else
                            {
                                PlayMoveAnimation();
                            }
                        }
                        else
                        {
                            _stateMachine.ChangeState(_attackState);
                        }
                    }
                },
                onExit: () =>
                {
                });

            _attackState = new OnAttack(
                onEnter: () =>
                {
                },
                onUpdate: () =>
                {
                    if (_isAnimationEnd)
                    {
                        if (_attackTarget is null)
                        {
                            if (_chaseTarget is null)
                            {
                                _stateMachine.ChangeState(_idleState);
                            }
                            else
                            {
                                _stateMachine.ChangeState(_chaseState);
                            }
                        }
                        else
                        {
                            PlayAttackAnimation();
                        }
                    }
                },
                onExit: () =>
                {
                });
        }

        private void Start()
        {
            _stateMachine.InitializeState(_idleState);
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        private void OnDestroy()
        {
            #region Animation
                animation.AnimationState.Event -= AnimationEvent;
            #endregion
            
            #region Attack
                attackDetectArea.OnEnterDetect += OnObjectEnterAttackDetectArea;
                attackDetectArea.OnExitDetect += OnObjectExitAttackDetectArea;
            #endregion
            
            #region Detect
                chaseDetectArea.OnEnterDetect -= OnObjectEnterChaseDetectArea;
                chaseDetectArea.OnExitDetect -= OnObjectExitChaseDetectArea;
            #endregion
        }

        private void OnJump()
        {
            StartMove();
        }

        private void OnGround()
        {
            StopMove();
        }

        private void OnAttack()
        {
            _attackTarget?.GetComponent<IAttackable>().TakeDamage(0);
        }
    }
}