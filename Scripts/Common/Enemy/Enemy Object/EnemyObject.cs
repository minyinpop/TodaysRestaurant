using Common.Enemy.Data;
using Common.Enemy.Enemy_Object.State_Machine;
using Common.Enemy.Enemy_Object.State_Machine.State;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private EnemySO enemyData;
        
        private readonly StateMachine _stateMachine = new();

        private IState _idleState;
        private IState _chaseState;
        private IState _attackState;
        
        private void Awake()
        {
            if (enemyData == null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(enemyData)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            #region Animation
                if (animation == null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
                
                animation.AnimationState.Event += OnSpineEvent;
            #endregion

            #region Detect
                if (chaseDetectArea == null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(chaseDetectArea)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
                
                chaseDetectArea.OnEnterDetect += OnObjectEnterChaseDetectArea;
                _onEnterChaseDetectCleanupAction = () =>
                {
                    chaseDetectArea.OnEnterDetect -= OnObjectEnterChaseDetectArea;
                    _onEnterChaseDetectCleanupAction = null;
                };
                
                chaseDetectArea.OnExitDetect += OnObjectExitChaseDetectArea;
                _onExitChaseDetectCleanupAction = () =>
                {
                    chaseDetectArea.OnExitDetect -= OnObjectExitChaseDetectArea;
                    _onExitChaseDetectCleanupAction = null;
                };
            #endregion

            _idleState = new OnIdle(
                onEnter: () =>
                {
                    #region Animation
                        PlayIdleAnimation();
                    #endregion
                },
                onUpdate: () =>
                {
                },
                onExit: () =>
                {
                });

            _chaseState = new OnChase(
                onEnter: () =>
                {
                    #region Animation
                        PlayMoveAnimation();
                    #endregion

                    #region Move
                        StartMove();
                    #endregion
                },
                onUpdate: () =>
                {
                    #region Move
                        KeepMoving();
                    #endregion
                },
                onExit: () =>
                {
                    StopMove();
                });

            _attackState = new OnAttack(
                onEnter: () =>
                {
                },
                onUpdate: () =>
                {
                },
                onExit: () =>
                {
                });
        }

        private void Start()
        {
            InitializeState();
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        private void OnDestroy()
        {
            #region Animation
                animation.AnimationState.Event -= OnSpineEvent;
            #endregion
            
            #region Detect
                _onEnterChaseDetectCleanupAction.Invoke();
                _onExitChaseDetectCleanupAction.Invoke();
            #endregion
        }
        
        #region StateMachine
            private void InitializeState()
            {
                _stateMachine.InitializeState(_idleState);
            }
            
            private void StartChaseState()
            {
                _stateMachine.ChangeState(_chaseState);
            }
            
            private void StopChaseState()
            {
                _stateMachine.ChangeState(_idleState);
            }
        #endregion
    }
}