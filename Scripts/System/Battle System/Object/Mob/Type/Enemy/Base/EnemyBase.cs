using System.Battle_System.Object.Mob.Type.Enemy.System;
using Data.General.Damage.Base;
using Data.Mob.Enemy.Base;
using General;
using UnityEngine;

namespace System.Battle_System.Object.Mob.Type.Enemy.Base
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

        public void Hurt(int damage, Action isAlive, Action isDeath)
        {
            if (IsDeath) return;
            HealthBar.Subtract(damage,
                isAlive: () =>
                {
                    AnimationSystem.Hurt(() =>
                    {
                        Debug.Log($"{name} is alive.");
                        AnimationSystem.Idle();
                        isAlive?.Invoke();
                    });
                },
                isDeath: () =>
                {
                    AnimationSystem.Death(() =>
                    {
                        Debug.Log($"{name} is dead.");
                        IsDeath = true;
                        isDeath?.Invoke();
                    });
                });
        }
    }
}