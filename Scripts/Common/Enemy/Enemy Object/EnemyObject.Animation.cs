using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;
using Event = Spine.Event;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Animation Settings")]
        [field: SerializeField] private new SkeletonAnimation animation;
        [field: SerializeField] private SpineAnimation idleAnimation;
        [field: SerializeField] private SpineAnimation moveAnimation;
        [field: SerializeField] private SpineAnimation attackAnimation;

        private bool _isAnimationEnd = true;

        private void PlayIdleAnimation()
        {
            idleAnimation.GetValues(out var layer, out var animationName, out var loop);
            animation.AnimationState.SetAnimation(layer, animationName, loop);
        }

        private void PlayMoveAnimation()
        {
            _isAnimationEnd = false;
            
            moveAnimation.GetValues(out var layer, out var animationName, out _);
            var trackEntry = animation.AnimationState.SetAnimation(layer, animationName, false);

            AnimationState.TrackEntryDelegate handler = null;
            handler = entry =>
            {
                entry.Complete -= handler;
                
                _isAnimationEnd = true;
            };
            trackEntry.Complete += handler;
        }

        private void PlayAttackAnimation()
        {
            _isAnimationEnd = false;
            
            attackAnimation.GetValues(out var layer, out var animationName, out _);
            var trackEntry = animation.AnimationState.SetAnimation(layer, animationName, false);
            
            AnimationState.TrackEntryDelegate handler = null;
            handler = entry =>
            {
                entry.Complete -= handler;
                
                _isAnimationEnd = true;
            };
            trackEntry.Complete += handler;
        }

        private void AnimationEvent(TrackEntry trackEntry, Event @event)
        {
            switch (@event.Data.Name)
            {
                case "Jump":
                {
                    OnJump();
                    break;
                }
                case "Ground":
                {
                    OnGround();
                    break;
                }
                case "Attack":
                {
                    if (OnAttack is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {nameof(OnAttack)} cannot be null.");
                        Destroy(gameObject);
                        return;
                    }

                    OnAttack.Invoke(_battleEnemyEntry);
                    break;
                }
            }
        }
    }
}