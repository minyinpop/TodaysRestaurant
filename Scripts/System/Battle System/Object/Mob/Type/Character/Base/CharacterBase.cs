using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Mob.Type.Character.System;
using Data.Animation.Spine;
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
        [field: SerializeField] private CharacterSO characterData;

        private bool IsDeath;
        
        public static event Action<BattleCard, Action, Action> OnAttack;

        private void Start()
        {
            characterData.GetHealthValues(out var min, out var max);
            HealthBar.Init(min, max);
        }

        private void OnEnable()
        {
            AnimationSystem.Idle();
        }

        public void Attack(BattleCard card, SkeletonAnimationSettings settings, Action haveEnemyAlive, Action enemyAllDeath)
        {
            AnimationSystem.Attack(settings,
            onAttackPoint: () =>
            {
                OnAttack?.Invoke(card, haveEnemyAlive, enemyAllDeath);
            },
            onComplete: AnimationSystem.Idle);
        }

        public void Hurt(float damage, Action isDeath = null, Action onComplete = null)
        {
            if (IsDeath) return;
            HealthBar.Subtract(damage,
                isAlive: () =>
                {
                    AnimationSystem.Hurt(() =>
                    {
                        AnimationSystem.Idle();
                        onComplete?.Invoke();
                    });
                },
                isDeath: () =>
                {
                    AnimationSystem.Death(() =>
                    {
                        IsDeath = true;
                        isDeath?.Invoke();
                        onComplete?.Invoke();
                    });
                });
        }
    }
}