using System;
using Common.Enemy.Data;
using Common.Enemy.Enemy_Object.State_Machine;
using Common.Enemy.Enemy_Object.State_Machine.State;
using Common.Level.Child.Level_Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private NavMeshAgent _agent;
        
        [field: Header("Data")]
        [field: SerializeField] private EnemySO enemyData;

        private bool _initialized;
        
        private BattleEnemyEntry _battleEnemyEntry;
        
        private readonly StateMachine _stateMachine = new();

        private IState _idleState;
        private IState _chaseState;
        private IState _attackState;

        public static event Action<BattleEnemyEntry> OnAttack;
        
        private void Awake()
        {
            if (_agent is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_agent)} cannot be null.");
                Destroy(gameObject);
                return;
            }

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
            if (_agent is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_agent)} is null, but you try to use {nameof(OnJump)}");
                return;
            }

            StartMove();
        }

        private void OnGround()
        {
            if (_agent is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_agent)} is null, but you try to use {nameof(OnGround)}");
                return;
            }

            StopMove();
        }

        public void Initialize(Vector3 spawnPoint, BattleEnemyEntry entry)
        {
            if (_initialized)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(Initialize)} > {nameof(_initialized)} is already initialize.");
                Destroy(gameObject);
                return;
            }

            _initialized = true;

            _battleEnemyEntry = entry;

            _agent.Warp(spawnPoint);
            
            _stateMachine.InitializeState(_idleState);
        }
    }
}