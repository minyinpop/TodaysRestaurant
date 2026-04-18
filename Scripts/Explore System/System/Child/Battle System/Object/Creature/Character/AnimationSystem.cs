using System;
using System.Collections;
using Animation_System.Spine;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using Event = Spine.Event;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Character
{
    public sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("動畫組件")]
        [field: SerializeField, FormerlySerializedAs("_skeletonAnimation")] private SkeletonAnimation skeletonAnimation;
        
        [field: Header("動畫設定")]
        [field: SerializeField, FormerlySerializedAs("IdleAnima")] private SpineAnimation idleSpine;
        [field: SerializeField, FormerlySerializedAs("HurtAnima")] private SpineAnimation hurtSpine;
        [field: SerializeField, FormerlySerializedAs("DeadAnima")] private SpineChainAnimation deathSpine;
        
        private IEnumerator _mainCoroutine;
        
        private SpineAnimation _currentAttackSpine;
        private SpineAnimation _nextAttackSpine;
        
        private void OnDisable()
        {
            if (_mainCoroutine is not null)
            {
                StopCoroutine(_mainCoroutine);
                _mainCoroutine = null;
            }
        }

        public void Idle()
        {
            idleSpine.GetValues(out var layer, out var animationName, out var loop);
            skeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        public void Attack(SpineAnimation anima, Action onAttackPoint, Action onComplete)
        {
            _mainCoroutine = AttackCoroutine();
            StartCoroutine(_mainCoroutine);
            return;
            
            IEnumerator AttackCoroutine()
            {
                if (_currentAttackSpine is null)
                {
                    // Debug.Log("Play Current Attack.");
                    
                    _currentAttackSpine = anima;
                }
                else
                {
                    // Debug.Log("Wait Current Attack End.");
                    
                    _nextAttackSpine = anima;
                    
                    yield return new WaitUntil(() => _currentAttackSpine is null);
                    
                    // Debug.Log("Change Next Attack To Current And Play.");
                    
                    _currentAttackSpine = _nextAttackSpine;
                    _nextAttackSpine = null;
                }
                
                anima.GetValues(out var layer, out var animationName, out var loop);
                
                var entry = skeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
                    entry.Event += OnAttackPoint;
                    entry.Complete += OnComplete;
                
                yield break;
                
                void OnAttackPoint(TrackEntry entry, Event @event)
                {
                    entry.Event -= OnAttackPoint;
                    onAttackPoint.Invoke();
                }
                
                void OnComplete(TrackEntry entry)
                {
                    entry.Complete -= OnComplete;
                    
                    _currentAttackSpine = null;

                    if (_nextAttackSpine is null)
                    {
                        // Debug.Log("All Attack Complete.");
                        
                        onComplete.Invoke();
                    }
                }
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
            _mainCoroutine = DeadCoroutine();
            StartCoroutine(_mainCoroutine);
            return;
            
            IEnumerator DeadCoroutine()
            {
                onComplete.Invoke();
                deathSpine.GetValues(out var animas);
                var complete = false;
                foreach (var anima in animas)
                {
                    anima.GetValues(out var layer, out var animationName, out var loop);
                    
                    var entry = skeletonAnimation.AnimationState.SetAnimation(layer, animationName, loop);
                        entry.Complete += OnComplete;
                    
                    yield return new WaitUntil(() => complete);
                    
                    continue;
                    
                    void OnComplete(TrackEntry entry)
                    {
                        entry.Complete -= OnComplete;
                        complete = true;
                    }
                }
            }
        }
    }
}