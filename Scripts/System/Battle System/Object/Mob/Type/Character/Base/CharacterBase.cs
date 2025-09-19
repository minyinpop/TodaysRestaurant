using System.Battle_System.Object.Card.Base;
using System.Battle_System.Object.Mob.Type.Character.System;
using System.Collections;
using System.Collections.Generic;
using Data.Animation.Spine;
using Data.General;
using Data.Mob.Character.Base;
using General;
using UnityEngine;

namespace System.Battle_System.Object.Mob.Type.Character.Base
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

        private bool IsDeath;
        
        public static event Action<ICard, Action, Action> OnAttack;
        public static event Action<List<CardType>, Action> RecycleCard;

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
                if (IsDeath) return;
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
                        CurrentCor = RecycleCardCoroutine(
                            onComplete: () =>
                            {
                                IsDeath = true;
                                isDeath?.Invoke();
                            });
                        StartCoroutine(CurrentCor);
                        AnimationSystem.Death();
                        return;
                        
                        IEnumerator RecycleCardCoroutine(Action onComplete)
                        {
                            var complete = false;
                            CharacterData.GetUseCardType(out var cardTypes);
                            RecycleCard?.Invoke(cardTypes, () => complete = true);
                            yield return new WaitUntil(() => complete);
                            onComplete?.Invoke();
                            CurrentCor = null;
                        }
                    });
            }
        #endregion
    }
}