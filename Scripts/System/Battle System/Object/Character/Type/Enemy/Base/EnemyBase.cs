using System.Battle_System.Object.Character.Type.Enemy.System;
using Data.Character.Enemy.Base;
using Data.General.Damage.Base;
using General;
using UnityEngine;

namespace System.Battle_System.Object.Character.Type.Enemy.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class EnemyBase : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private EnemySO EnemyData;

        private bool IsDeath;

        public static event Action<Damage, Action> OnAttack;

        private void Start()
        {
            EnemyData.GetHealthValues(out var min, out var max);
            HealthBar.Init(min, max);
        }

        private void OnEnable()
        {
            AnimationSystem.Idle();
        }

        public void Attack(Action onComplete = null)
        {
            AnimationSystem.Attack(
            onAttackPoint: () =>
            {
                EnemyData.GetDamage(out var damage);
                OnAttack?.Invoke(damage, onComplete);
            },
            onComplete: AnimationSystem.Idle);
        }

        public void Hurt(int damage, Action isDeath = null, Action onComplete = null)
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