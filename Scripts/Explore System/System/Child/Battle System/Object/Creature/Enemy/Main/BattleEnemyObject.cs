using System;
using Common.Enemy.Data;
using Common.Status_Bar;
using Common.Value;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main
{
    [RequireComponent(typeof(AnimationSystem))]
    public abstract class BattleEnemyObject : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] protected new AnimationSystem animation;
        
        [field: Header("Object")]
        [field: SerializeField] protected StatusBar healthBar;
        
        [field: Header("Data")]
        [field: SerializeField] protected EnemySO enemyData;

        protected int _health;

        public static event Action<Damage, Action, Action> OnAttack;
        
        public bool death { get; protected set; }

        private void Awake()
        {
            #region 必要條件檢查
                if (animation is null)
                {
                    throw new InvalidOperationException(nameof(animation));
                }
                
                if (healthBar is null)
                {
                    throw new InvalidOperationException(nameof(healthBar));
                }

                if (enemyData is null)
                {
                    throw new InvalidOperationException(nameof(enemyData));
                }
            #endregion

            _health = enemyData.Health;
        }

        private void Start()
        {
            #region 設定血條
                healthBar.Initialize(_health, _health);
            #endregion
            
            #region 設定動畫
                animation.Idle();
            #endregion
        }
        
        public abstract void Attack(Action haveCharacterAlive, Action characterAllDead);
        
        public abstract void Hurt(int damage, Action isAlive, Action isDeath);

        protected void InvokeOnAttack(Damage damage, Action haveCharacterAlive, Action characterAllDead)
        {
            if (OnAttack is null)
            {
                throw new InvalidOperationException($"沒有 class 訂閱 {nameof(OnAttack)}。");
            }
            
            OnAttack.Invoke(damage, haveCharacterAlive, characterAllDead);
        }
    }
}