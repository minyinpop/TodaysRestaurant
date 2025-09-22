using System.Collections;
using Data.Animation.Spine;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;
using SpineAnimation = Data.Animation.Spine.SpineAnimation;

namespace System.Battle.Object.Mob.Type.Character.System
{
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Skeleton Animation Settings")]
        [field: SerializeField] private SpineAnimation IdleAnima;
        [field: SerializeField] private SpineAnimation HurtAnima;
        [field: SerializeField] private SpineChainAnimation DeadAnima;
        
        private TrackEntry CurrentEntry;

        private IEnumerator DeadCor;
        
        private void OnDisable()
        {
            if (DeadCor is not null)
            {
                StopCoroutine(DeadCor);
                DeadCor = null;
            }
        }

        public void Idle()
        {
            IdleAnima.GetValues(out var layer, out var animationName, out var loop);
            CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        public void Attack(SpineAnimation anima, Action onAttackPoint, Action onComplete)
        {
            anima.GetValues(out var layer, out var animationName, out var loop);
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
            DeadCor = DeadCoroutine();
            StartCoroutine(DeadCor);
            return;
            
            IEnumerator DeadCoroutine()
            {
                onComplete?.Invoke();
                DeadAnima.GetValues(out var animas);
                var complete = false;
                foreach (var anima in animas)
                {
                    anima.GetValues(out var layer, out var animationName, out var loop);
                    CurrentEntry = SkeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
                    CurrentEntry.Complete += OnComplete;
                    yield return new WaitUntil(() => complete);
                    continue;
                    
                    void OnComplete(TrackEntry entry)
                    {
                        CurrentEntry.Complete -= OnComplete;
                        complete = true;
                    }
                }
            }
        }
    }
}