using System.Battle_System.Object.Card.Type.Battle.System.Main;
using Data.Attribute;
using Data.Player;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class PlayerSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;
        
        [field: Header("Animation Name")]
        [field: SerializeField] private SkeletonAnimationValue IdleAnimation;

        private void Start()
        {
            Idle();
        }

        private void OnEnable()
        {
            BattleCard.OnUse += Attack;
        }

        private void OnDisable()
        {
            BattleCard.OnUse -= Attack;
        }

        private void Idle()
        {
            IdleAnimation.GetValues(out var layer, out var animationName, out var loop);
            SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        private void Attack(BattleCard card, SkeletonAnimationValue value, Action onComplete = null)
        {
            value.GetValues(out var layer, out var animationName, out var loop);
            var entry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
            entry.Complete += Entry;
            return;

            void Entry(TrackEntry _)
            {
                entry.Complete -= Entry;
                Idle();
                onComplete?.Invoke();
            }
        }
    }
}