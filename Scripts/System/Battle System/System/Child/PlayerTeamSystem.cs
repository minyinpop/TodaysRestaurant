using System.Battle_System.Object.Card.Base.Card_Type;
using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Character.Type.Enemy.Base;
using System.Battle_System.Object.Character.Type.Friendly.Base;
using System.Collections.Generic;
using Data.Animation.Spine;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class PlayerTeamSystem : MonoBehaviour
    {
        [field: Header("Character")]
        [field: SerializeField] private FriendlyBase Bernard;
        [field: SerializeField] private FriendlyBase Ray;
        [field: SerializeField] private FriendlyBase Muu;

        private List<FriendlyBase> CharacterOrder = new();

        private void Start()
        {
            CharacterOrder.Add(Bernard);
        }

        private void OnEnable()
        {
            BattleCard.OnUse += OnBattleCardUse;
            EnemyBase.OnAttack += OnEnemyAttack;
        }
        
        private void OnDisable()
        {
            BattleCard.OnUse -= OnBattleCardUse;
            EnemyBase.OnAttack -= OnEnemyAttack;
        }

        private void OnBattleCardUse(BattleCard card, SkeletonAnimationSettings settings, Action onComplete = null)
        {
            switch (card)
            {
                case IForkCard:
                {
                    Bernard.Attack(card, settings, onComplete);
                    break;
                }
                case ISpoonCard:
                {
                    Ray.Attack(card, settings, onComplete);
                    break;
                }
            }
        }

        private void OnEnemyAttack(float damage, Action onComplete = null)
        {
            CharacterOrder[0].Hurt(damage, onComplete);
        }
    }
}