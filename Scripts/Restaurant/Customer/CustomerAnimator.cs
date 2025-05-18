using Spine.Unity;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class CustomerAnimator : MonoBehaviour
    {
        private AnimationReferenceAsset IdleClip { get; set; }
        private AnimationReferenceAsset WalkClip { get; set; }
        private AnimationReferenceAsset SitClip { get; set; }
        
        private SkeletonAnimation SkeletonAnima { get; set; }

        private void Awake()
        {
            SkeletonAnima = GetComponent<SkeletonAnimation>();
        }

        public void Init(AnimationReferenceAsset idle, AnimationReferenceAsset walk, AnimationReferenceAsset sit)
        {
            IdleClip = idle;
            WalkClip = walk;
            SitClip = sit;
        }
        
        public void PlayIdleAnima()
        {
            // TODO
        }
        
        public void PlayWalkAnima()
        {
            SkeletonAnima.AnimationState.SetAnimation(1, WalkClip, true);
        }

        public void PlaySitAnima()
        {
            SkeletonAnima.AnimationState.SetAnimation(1, SitClip, false);
        }

        public void SetFlipX(bool isFlip)
        {
            if (isFlip)
                SkeletonAnima.skeleton.ScaleX = -1;
            else
                SkeletonAnima.skeleton.ScaleX = 1;
        }
    }
}