using Common.Enemy.Data;
using Common.Enemy.Object.State_Machine;
using Common.Enemy.Object.State_Machine.State;
using UnityEngine;

namespace Common.Enemy.Object
{
    public partial class EnemyObject : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private EnemySO enemyData;
        
        private readonly StateMachine _stateMachine = new();

        private IState _idleState;
        private IState _moveState;
        
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
            #endregion

            #region Detect
                if (detectArea == null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(detectArea)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
                
                detectArea.OnEnterDetect += OnObjectEnterDetect;
                _onEnterDetectCleanupAction = () =>
                {
                    detectArea.OnEnterDetect -= OnObjectEnterDetect;
                    _onEnterDetectCleanupAction = null;
                };
                
                detectArea.OnExitDetect += OnObjectExitDetect;
                _onExitDetectCleanupAction = () =>
                {
                    detectArea.OnExitDetect -= OnObjectExitDetect;
                    _onExitDetectCleanupAction = null;
                };
            #endregion

            _idleState = new OnIdle(
                onEnter: () =>
                {
                    PlayIdleAnimation();
                },
                onExit: () =>
                {
                });

            _moveState = new OnMove(
                onEnter: () =>
                {
                    PlayMoveAnimation();
                },
                onExit: () =>
                {
                });
        }

        private void Start()
        {
            InitializeState();
        }

        private void OnDestroy()
        {
            _onEnterDetectCleanupAction.Invoke();
            _onExitDetectCleanupAction.Invoke();
        }
        
        #region StateMachine
            private void InitializeState()
            {
                _stateMachine.InitializeState(_idleState);
            }

            private void IdleState()
            {
                _stateMachine.ChangeState(_idleState);
            }
            
            private void MoveState()
            {
                _stateMachine.ChangeState(_moveState);
            }
        #endregion
    }
}