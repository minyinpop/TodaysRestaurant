using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Character.Type.Enemy.System;
using Data.Character.Enemy.Base;
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

        public static event Action<float, Action> OnAttack;

        private void Start()
        {
            EnemyData.GetHealthValues(out var min, out var max);
            HealthBar.Init(min, max);
        }

        public void OnEnable()
        {
            Idle();
        }

        public void Idle()
        {
            AnimationSystem.Idle();
        }

        public void Attack(Action onComplete = null)
        {
            AnimationSystem.Attack(
            onAttackPoint: () =>
            {
                EnemyData.GetDamageValues(out var damage);
                OnAttack?.Invoke(damage, onComplete);
            },
            onComplete: Idle);
        }

        public void Hurt(BattleCard card, Action onComplete = null)
        {
            card.GetDamage(out var damage);
            HealthBar.Subtract(damage);
            
            AnimationSystem.Hurt(() =>
            {
                Idle();
                onComplete?.Invoke();
            });
        }
    }
}