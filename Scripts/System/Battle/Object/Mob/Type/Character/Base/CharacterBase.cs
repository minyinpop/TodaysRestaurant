using System.Battle.Object.Card.Base;
using System.Battle.Object.Mob.Type.Character.System;
using System.Collections;
using Data.Animation.Spine;
using Data.Mob.Character.Base;
using General;
using UnityEngine;

namespace System.Battle.Object.Mob.Type.Character.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class CharacterBase : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private CharacterSO CharacterData;
        
        public static event Action<ICard, Action, Action> OnAttack;

        private IEnumerator CurrentCor;

        private void Start()
        {
            CharacterData.GetHealthValues(out var min, out var max);
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
            public void GetCharacterData(out CharacterSO data)
            {
                data = CharacterData;
            }
        #endregion

        #region Attack
            public void Attack(ICard card, SkeletonAnimationSettings settings, Action haveEnemyAlive, Action enemyAllDead)
            {
                AnimationSystem.Attack(settings,
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
                        AnimationSystem.Death(
                            onComplete: () =>
                            {
                                isDeath?.Invoke();
                            });
                    });
            }
        #endregion
    }
}