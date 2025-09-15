using System.Battle_System.Object.Card.Type.Battle.System.Main;
using Data.Animation.Spine;
using Data.General;
using Object;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace System.Battle_System.Object.Character
{
    internal sealed class Friendly : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Status")]
        [field: SerializeField] private Health Health;
        
        [field: Header("Animation Name")]
        [field: SerializeField] private SkeletonAnimationSettings idleAnimationSettings;

        private void Start()
        {
            Idle();
        }

        private void Idle()
        {
            idleAnimationSettings.GetValues(out var layer, out var animationName, out var loop);
            SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        public void Attack(BattleCard card, SkeletonAnimationSettings value, Action onComplete = null)
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