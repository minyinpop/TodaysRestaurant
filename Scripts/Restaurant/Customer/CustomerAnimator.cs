using System.Collections;
using Database.Restaurant.Customer.Attribute;
using Spine.Unity;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class CustomerAnimator : MonoBehaviour
    {
        [field: Header("屬性資料")]
        [field: SerializeField] private CustomerAttributeSO CustomerAttribute { get; set; }
        
        [field: Header("主要的 Spine 動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset Walk { get; set; }
        [field: SerializeField] private AnimationReferenceAsset Sit { get; set; }
        
        [field: Header("次要的 Spine 動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset Blink { get; set; }
        
        private SkeletonAnimation SkeletonAnimation { get; set; }
        
        private IEnumerator BlinkCoroutine { get; set; }

        private void Awake()
        {
            SkeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void Start()
        {
            BlinkCoroutine = BlinkProcess();
            StartCoroutine(BlinkCoroutine);
        }

        private void OnDisable()
        {
            if (BlinkCoroutine is not null)
            {
                StopCoroutine(BlinkCoroutine);
                BlinkCoroutine = null;
            }
        }

        private IEnumerator BlinkProcess()
        {
            while (true)
            {
                SkeletonAnimation.AnimationState.SetAnimation(2, Blink, false);
                
                var durationTime = Blink.Animation.Duration + CustomerAttribute.AnimationAttribute.GetRandomBlinkTime();
                yield return new WaitForSeconds(durationTime);
            }
        }
    }
}