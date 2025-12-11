using System;
using System.Collections;
using Animation_System.Spine;
using Battle_System.Object.Card;
using Battle_System.Object.Creature.Crew.Data;
using Common.Object;
using UnityEngine;

namespace Battle_System.Object.Creature.Crew
{
    [RequireComponent(typeof(AnimationSystem))]
    internal class Crew : Creature
    {
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private CrewSO CrewData;
        
        public static event Action<ICard, Action, Action> OnAttack;

        private IEnumerator CurrentCor;

        private void Start()
        {
            CrewData.GetHealth(out var min, out var max);
            HealthBar.Init(min, max);
        }

        private void OnEnable()
        {
            AnimationSystem.Idle();
        }

        private void OnDisable()
        {
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
            }
        }

        #region Data
            public void GetCharacterData(out ICreature data)
            {
                data = CrewData;
            }
        #endregion

        #region Attack
            public void Attack(ICard card, SpineAnimation animation, Action haveEnemyAlive, Action enemyAllDead)
            {
                AnimationSystem.Attack(animation,
                    onAttackPoint: () =>
                    {
                        OnAttack?.Invoke(card, haveEnemyAlive, enemyAllDead);
                    },
                    onComplete: () =>
                    {
                        AnimationSystem.Idle();
                    });
            }
        #endregion

        #region Hurt
            public void Hurt(float damage, Action isAlive, Action isDeath)
            {
                HealthBar.Subtract(damage,
                    isAlive: () =>
                    {
                        AnimationSystem.Hurt(() =>
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