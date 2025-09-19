using Data.Animation.Spine;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace System.Battle_System.Object.Mob.Type.Character.System
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Skeleton Animation Settings")]
        [field: SerializeField] private SkeletonAnimationSettings IdleAnimationSettings;
        [field: SerializeField] private SkeletonAnimationSettings HurtAnimationSettings;
        [field: SerializeField] private SkeletonAnimationSettings DeathAnimationSettings;
        
        private TrackEntry CurrentEntry;

        public void Idle()
        {
            IdleAnimationSettings.GetValues(out var layer, out var animationName, out var loop);
            CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        public void Attack(SkeletonAnimationSettings settings, Action onAttackPoint = null, Action onComplete = null)
        {
            settings.GetValues(out var layer, out var animationName, out var loop);
            CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
            CurrentEntry.Event += OnAttackPoint;
            CurrentEntry.Complete += OnComplete;
            return;
            
            void OnAttackPoint(TrackEntry entry, Event @event)
            {
                CurrentEntry.Event -= OnAttackPoint;
                onAttackPoint?.Invoke();
            }
            
            void OnComplete(TrackEntry entry)
            {
                CurrentEntry.Complete -= OnComplete;
                onComplete?.Invoke();
            }
        }

        public void Hurt(Action onComplete = null)
        {
            HurtAnimationSettings.GetValues(out var layer, out var animationName, out var loop);
            CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
            CurrentEntry.Complete += OnComplete;
            return;
            
            void OnComplete(TrackEntry entry)
            {
                CurrentEntry.Complete -= OnComplete;
                onComplete?.Invoke();
            }
        }
        
        public void Death(Action onComplete = null)
        {
            DeathAnimationSettings.GetValues(out var layer, out var animationName, out var loop);
            CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
            CurrentEntry.Complete += OnComplete;
            return;
            
            void OnComplete(TrackEntry entry)
            {
                CurrentEntry.Complete -= OnComplete;
                onComplete?.Invoke();
            }
        }
    }
}