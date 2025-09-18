using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Character.Type.Friendly.System;
using Data.Animation.Spine;
using Data.Character.Friendly.Base;
using General;
using UnityEngine;

namespace System.Battle_System.Object.Character.Type.Friendly.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class FriendlyBase : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private FriendlySO FriendlyData;

        private bool IsDeath;
        
        public static event Action<BattleCard, Action> OnAttack;

        private void Start()
        {
            FriendlyData.GetHealthValues(out var min, out var max);
            HealthBar.Init(min, max);
        }

        private void OnEnable()
        {
            AnimationSystem.Idle();
        }

        public void Attack(BattleCard card, SkeletonAnimationSettings settings, Action onComplete = null)
        {
            AnimationSystem.Attack(settings,
            onAttackPoint: () =>
            {
                OnAttack?.Invoke(card, onComplete);
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