using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Spine.Unity;
using UnityEngine;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace Player_System.Object.Character
{
    [RequireComponent(typeof(DoAnimation))]
    public partial class PlayerCharacter
    {
        [field: Header("Animation System")]
        [field: SerializeField] private GameObject rendererRoot;
        [field: SerializeField] private SkeletonAnimation skeletonAnimation;
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private SpineAnimation idle;
        [field: SerializeField] private SpineAnimation walk;
        [field: SerializeField] private DoRotate turnLeft;
        [field: SerializeField] private DoRotate turnRight;

        private void SetAnimation(SpineAnimation anima)
        {
            anima.GetValues(out var layer, out var animaName, out var loop);
            skeletonAnimation.AnimationState.SetAnimation(layer, animaName, loop);
        }

        private void PlayIdleAnimation()
        {
            SetAnimation(idle);
        }
        
        private void PlayWalkAnimation()
        {
            SetAnimation(walk);
        }

        private void TurnsLeft()
        {
            animation.DoRotate(rendererRoot.transform, turnLeft);
        }
        
        private void TurnsRight()
        {
            animation.DoRotate(rendererRoot.transform, turnRight);
        }
    }
}