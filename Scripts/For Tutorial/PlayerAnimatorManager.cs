using System.Collections;
using Spine.Unity;
using UnityEngine;

namespace For_Tutorial
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        [field: Header("動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset Idle { get; set; }
        [field: SerializeField] private AnimationReferenceAsset Walk { get; set; }
        [field: SerializeField] private AnimationReferenceAsset Run { get; set; }
        [field: SerializeField] private AnimationReferenceAsset Blink { get; set; }
        
        private SkeletonAnimation SkeletonAnimation { get; set; }
        
        private void Awake() => SkeletonAnimation = GetComponent<SkeletonAnimation>();

        private void OnEnable()
        {
            EyeCoroutine = BlinkProcess();
            StartCoroutine(EyeCoroutine);
        }

        private void OnDisable()
        {
            if (EyeCoroutine is null)
                return;
            StopCoroutine(EyeCoroutine);
            EyeCoroutine = null;
        }
        
        public void PlayIdleClip() => SkeletonAnimation.AnimationState.SetAnimation(1, Idle, true);
        public void PlayWalkClip() => SkeletonAnimation.AnimationState.SetAnimation(1, Walk, true);
        public void PlayRunClip() => SkeletonAnimation.AnimationState.SetAnimation(1, Run, true);
        
        public void SetFlipX(bool isFlipX) => SkeletonAnimation.Skeleton.ScaleX = isFlipX ? -1 : 1;

        private IEnumerator EyeCoroutine { get; set; }
        private IEnumerator BlinkProcess()
        {
            while (true)
            {
                SkeletonAnimation.AnimationState.SetAnimation(2, Blink, false);
                var duration = Blink.Animation.Duration + Random.Range(5f, 12f);
                yield return new WaitForSeconds(duration);
            }
        }
    }
}