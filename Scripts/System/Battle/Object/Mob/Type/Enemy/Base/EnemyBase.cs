using System.Battle.Object.Mob.Type.Enemy.System;
using Common;
using Data.Battle_System.Creature.Interface;
using Data.General;
using UnityEngine;

namespace System.Battle.Object.Mob.Type.Enemy.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class EnemyBase : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private IBattleCreature EnemyData;

        public static event Action<Damage, Action, Action> OnAttack;

        private void Start()
        {
            EnemyData.GetHealth(out var min, out var max);
            HealthBar.Init(min, max);
            
            AnimationSystem.Idle();
        }

        #region Attack
            public void Attack(Action haveCharacterAlive, Action characterAllDead)
            {
                AnimationSystem.Attack(
                    onAttackPoint: () =>
                    {
                        EnemyData.GetDamage(out var damage);
                        OnAttack?.Invoke(damage,
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