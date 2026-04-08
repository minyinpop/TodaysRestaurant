using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main
{
    public sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Skeleton Animation Settings")]
        [field: SerializeField] private SpineAnimation IdleAnima;
        [field: SerializeField] private SpineAnimation AttackAnima;
        [field: SerializeField] private SpineAnimation HurtAnima;
        [field: SerializeField] private SpineAnimation DeadAnima;
        
        private TrackEntry CurrentEntry;
        
        public void Idle()
        {
            IdleAnima.GetValues(out var layer, out var animationName, out var loop);
            CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        public void Attack(Action onAttackPoint, Action onComplete)
        {
            AttackAnima.GetValues(out var layer, out var animationName, out var loop);
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

        public void Hurt(Action onComplete)
        {
            HurtAnima.GetValues(out var layer, out var animationName, out var loop);
            CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
            CurrentEntry.Complete += OnComplete;
            return;
            
            void OnComplete(TrackEntry entry)
            {
                CurrentEntry.Complete -= OnComplete;
                onComplete?.Invoke();
            }
        }

        public void Dead(Action onComplete)
        {
            DeadAnima.GetValues(out var layer, out var animationName, out var loop);
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