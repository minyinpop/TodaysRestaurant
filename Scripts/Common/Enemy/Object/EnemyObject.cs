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
            #endregion

            _idleState = new OnIdle(
                onEnter: () =>
                {
                    PlayIdleAnimation();
                },
                onExit: () =>
                {
                });
        }

        private void Start()
        {
            _stateMachine.InitializeState(_idleState);
        }
    }
}