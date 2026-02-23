using System;
using System.Collections;
using Animation_System.Spine;
using Battle_System.Object.Card;
using Common.Character;
using Common.Status_Bar;
using UnityEngine;

namespace Battle_System.Object.Creature.Character
{
    [RequireComponent(typeof(AnimationSystem))]
    internal class Character : Creature
    {
        [field: Header("Systems")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Objects")]
        [field: SerializeField] private StatusBar healthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private CharacterSO characterData;
        
        public static event Action<ICard, Action, Action> OnAttack;

        private IEnumerator _currentCoroutine;

        private void Start()
        {
            healthBar.Initialize(characterData.Health);
        }

        private void OnEnable()
        {
            AnimationSystem.Idle();
        }

        private void OnDisable()
        {
            if (_currentCoroutine is not null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }

        #region Data
            public void GetCharacterData(out CharacterSO data)
            {
                data = characterData;
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
                healthBar.Subtract(damage,
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