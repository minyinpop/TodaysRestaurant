using Spine;
using Spine.Unity;
using UnityEngine;
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

        private void PlayIdleAnimation()
        {
            idleAnimation.GetValues(out var layer, out var animationName, out var loop);
            animation.AnimationState.SetAnimation(layer, animationName, loop);
        }
        
        private void PlayMoveAnimation()
        {
            moveAnimation.GetValues(out var layer, out var animationName, out var loop);
            animation.AnimationState.SetAnimation(layer, animationName, loop);
        }
        
        private void OnSpineEvent(TrackEntry trackEntry, Event e)
        {
            switch (e.Data.Name)
            {
                case "Jump":
                {
                    Debug.Log("Jump");
                    break;
                }
            }
        }
    }
}