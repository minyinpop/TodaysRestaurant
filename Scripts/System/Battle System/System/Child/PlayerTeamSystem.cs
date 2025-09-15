using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Card.Type.Battle.Type.Fork.Base;
using System.Battle_System.Object.Card.Type.Battle.Type.Spoon.Base;
using System.Battle_System.Object.Character;
using Data.Animation.Spine;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class PlayerTeamSystem : MonoBehaviour
    {
        [field: Header("Character")]
        [field: SerializeField] private Friendly Bernard;
        [field: SerializeField] private Friendly Ray;
        [field: SerializeField] private Friendly Muu;

        private void OnEnable()
        {
            BattleCard.OnUse += OnBattleCardUse;
        }
        
        private void OnDisable()
        {
            BattleCard.OnUse -= OnBattleCardUse;
        }

        private void OnBattleCardUse(BattleCard card, SkeletonAnimationSettings settings, Action onComplete = null)
        {
            switch (card)
            {
                case IFork:
                {
                    Bernard.Attack(card, settings, onComplete);
                    break;
                }
                case ISpoon:
                {
                    Ray.Attack(card, settings, onComplete);
                    break;
                }
            }
        }
    }
}