using System;
using Common.Data.Enemy;
using Common.Object;
using Common.Value;
using UnityEngine;

namespace Battle_System.Object.Creature.Enemy
{
    [RequireComponent(typeof(AnimationSystem))]
    internal class Enemy : Creature
    {
        [field: Header("Component")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private EnemySO enemyData;

        public static event Action<Damage, Action, Action> OnAttack;

        private void Start()
        {
            HealthBar.Init(enemyData.Health.Min, enemyData.Health.Max);
            
            AnimationSystem.Idle();
        }

        #region Attack
            public void Attack(Action haveCharacterAlive, Action characterAllDead)
            {
                AnimationSystem.Attack(
                    onAttackPoint: () =>
                    {
                        OnAttack?.Invoke(enemyData.Damage,
                            () =>
                            {
                                // haveCharacterAlive
                                haveCharacterAlive?.Invoke();
                            },
                            () =>
                            {
                                // characterAllDead
                                characterAllDead?.Invoke();
                            });
                    },
                    onComplete: () =>
                    {
                        AnimationSystem.Idle();
                    });
            }
        #endregion

        #region Hurt
            public void Hurt(int damage, Action isAlive, Action isDeath)
            {
                HealthBar.Subtract(damage,
                    isAlive: () =>
                    {
                        AnimationSystem.Hurt(
                            onComplete: () =>
                            {
                                AnimationSystem.Idle();
                                isAlive?.Invoke();
                            });
                    },
                    isDeath: () =>
                    {
                        AnimationSystem.Dead(
                            onComplete: () =>
                            {
                                isDeath?.Invoke();
                            });
                    });
            }
        #endregion
    }
}