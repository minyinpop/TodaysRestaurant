using System;
using Audio_System.Data;
using Common.Enemy.Data;
using Common.Status_Bar;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main
{
    [RequireComponent(typeof(AnimationSystem))]
    public abstract class BattleEnemyObject : MonoBehaviour
    {
        [field: Header("組件")]
        [field: SerializeField] protected new AnimationSystem animation;
        
        [field: Header("血條")]
        [field: SerializeField] protected StatusBar healthBar;
        
        [field: Header("敵人資料")]
        [field: SerializeField] protected EnemySO enemyData;
        
        [field: Header("音效資料")]
        [field: SerializeField] protected PlaySFXData attackSFXData;

        protected int _health;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EnemySO"> 敵人的資料 </param>
        /// <param name="Action"> 玩家隊伍的角色活著的回傳 </param>>
        /// <param name="Action"> 玩家隊伍的角色全部死亡的回傳 </param>>
        public static event Action<EnemySO, Action, Action> OnAttack;
        
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
        
        public abstract void Hurt(BattleCardSO battleCardData, Action isAlive, Action isDeath);

        protected void InvokeOnAttack(Action haveCharacterAlive, Action characterAllDead)
        {
            if (OnAttack is null)
            {
                throw new InvalidOperationException($"沒有 class 訂閱 {nameof(OnAttack)}。");
            }
            
            OnAttack.Invoke(enemyData, haveCharacterAlive, characterAllDead);
        }
    }
}