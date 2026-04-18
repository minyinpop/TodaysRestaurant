using System;
using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using Event = Spine.Event;
using Random = UnityEngine.Random;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace Common.Enemy_Battle_Object.Main
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("動畫組件")]
        [field: SerializeField, FormerlySerializedAs("SkeletonAnimation")] private SkeletonAnimation skeletonAnimation;
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("動畫設定")]
        [field: SerializeField, FormerlySerializedAs("IdleAnima")] private SpineAnimation idleSpine;
        [field: SerializeField] private List<SpineAnimation> attackSpines;
        [field: SerializeField, FormerlySerializedAs("HurtAnima")] private SpineAnimation hurtSpine;
        [field: SerializeField, FormerlySerializedAs("DeadAnima")] private SpineAnimation deathSpine;
        [field: SerializeField] private DoScale scaleDownSettings;

        private void Awake()
        {
            if (skeletonAnimation is null)
            {
                throw new InvalidOperationException($"{skeletonAnimation} 沒有被掛載。");
            }
            
            if (animation is null)
            {
                throw new InvalidOperationException($"{animation} 沒有被掛載。");
            }
        }

        public void Idle()
        {
            idleSpine.GetValues(out var layer, out var animationName, out var loop);
            skeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        public void Attack(Action onAttackPoint, Action onComplete)
        {
            var attackSpine = attackSpines[Random.Range(0, attackSpines.Count)];
            
            attackSpine.GetValues(out var layer, out var animationName, out var loop);
            
            var entry = skeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
                entry.Event += OnAttackPoint;
                entry.Complete += OnComplete;
            return;

            void OnAttackPoint(TrackEntry entry, Event @event)
            {
                entry.Event -= OnAttackPoint;
                onAttackPoint.Invoke();
            }
            
            void OnComplete(TrackEntry entry)
            {
                entry.Complete -= OnComplete;
                onComplete.Invoke();
            }
        }

        public void Hurt(Action onComplete)
        {
            hurtSpine.GetValues(out var layer, out var animationName, out var loop);
            
            var entry = skeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
                entry.Complete += OnComplete;
            return;
            
            void OnComplete(TrackEntry entry)
            {
                entry.Complete -= OnComplete;
                onComplete.Invoke();
            }
        }

        public void Dead(Action onComplete)
        {
            deathSpine.GetValues(out var layer, out var animationName, out var loop);
            
            var entry = skeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
                entry.Complete += OnComplete;
            return;
            
            void OnComplete(TrackEntry entry)
            {
                entry.Complete -= OnComplete;
                onComplete.Invoke();
                
                animation.DoScale_WorldSpace(
                    trans: transform,
                    settings: scaleDownSettings,
                    onComplete: () =>
                    {
                        Destroy(gameObject);
                    });
            }
        }
    }
}