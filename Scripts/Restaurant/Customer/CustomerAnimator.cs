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
        [field: SerializeField] private CustomerAttributeSO Attribute { get; set; }
        
        [field: Header("主要的 Spine 動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset Walk { get; set; }
        [field: SerializeField] private AnimationReferenceAsset Sit { get; set; }
        
        [field: Header("次要的 Spine 動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset Blink { get; set; }
        
        private SkeletonAnimation SkeletonAnimation { get; set; }
        private CustomerController CustomerController { get; set; }
        
        private IEnumerator BlinkCoroutine { get; set; }

        private void Awake()
        {
            SkeletonAnimation = GetComponent<SkeletonAnimation>();
            CustomerController = GetComponent<CustomerController>();
        }

        private void Start()
        {
            BlinkCoroutine = BlinkProcess();
            StartCoroutine(BlinkCoroutine);
        }

        private void OnEnable()
        {
            CustomerController.GoToSeat += GoToSeat;
            CustomerController.OnSeat += OnSeat;
            CustomerController.FlipX += FlipX;
        }

        private void OnDisable()
        {
            CustomerController.GoToSeat -= GoToSeat;
            CustomerController.OnSeat -= OnSeat;
            CustomerController.FlipX -= FlipX;
            
            if (BlinkCoroutine is not null)
            {
                StopCoroutine(BlinkCoroutine);
                BlinkCoroutine = null;
            }
        }

        private void GoToSeat() => SkeletonAnimation.AnimationState.SetAnimation(1, Walk, true);
        
        private void OnSeat() => SkeletonAnimation.AnimationState.SetAnimation(1, Sit, true);

        private void FlipX(bool isFlip) => SkeletonAnimation.Skeleton.ScaleX = isFlip ? -1 : 1;

        private IEnumerator BlinkProcess()
        {
            while (true)
            {
                SkeletonAnimation.AnimationState.SetAnimation(2, Blink, false);
                
                var durationTime = Blink.Animation.Duration + Attribute.AnimationAttribute.GetRandomBlinkTime();
                yield return new WaitForSeconds(durationTime);
            }
        }
    }
}