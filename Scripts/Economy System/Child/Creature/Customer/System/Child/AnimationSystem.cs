using Animation_System.DOTween;
using Spine.Unity;
using UnityEngine;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace Economy_System.Child.Creature.Customer.System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;

        [field: Header("Animation Settings")]
        [field: SerializeField] private SpineAnimation Idle_AnimationSettings;
        [field: SerializeField] private SpineAnimation Walk_AnimationSettings;
        [field: SerializeField] private SpineAnimation Sit_AnimationSettings;

        private void SetAnimation(SpineAnimation anima)
        {
            anima.GetValues(out var layer, out var animaName, out var loop);
            SkeletonAnimation.AnimationState.SetAnimation(layer, animaName, loop);
        }
        
        public void Idle() => SetAnimation(Idle_AnimationSettings);
        public void Walk() => SetAnimation(Walk_AnimationSettings);
        public void Sit() => SetAnimation(Sit_AnimationSettings);
    }
}