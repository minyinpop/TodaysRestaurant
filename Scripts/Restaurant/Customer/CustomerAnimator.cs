using System.Collections;
using Database.Restaurant.Customer.Attribute;
using Spine.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Restaurant.Customer
{
    // ==================================================
    // 用於執行顧客動畫的程式碼。
    // ==================================================
    
    [RequireComponent(typeof(CustomerManager))]
    [RequireComponent(typeof(SkeletonAnimation))]
    public class CustomerAnimator : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        [field: Header("玩家屬性資料"), SerializeField]
        public CustomerAttributeSO CustomerAttribute { get; private set; }
        
        
        
        // ========== { 自身組件 } ==========
        
        // 自身的 SkeletonAnimation 組件。
        private SkeletonAnimation SkeletonAnimation { get; set; }
        
        
        
        // ========== { 移動相關的動畫資產 } ==========
        
        [field: Header("移動相關的動畫資產"), Tooltip("玩家的閒置動畫。"), SerializeField]
        private AnimationReferenceAsset Idle { get; set; }
        
        [field: Tooltip("玩家的走路動畫。"), SerializeField]
        private AnimationReferenceAsset Walk { get; set; }
        
        
        
        // ========== { 細節相關的動畫資產 } ==========
        
        [field: Header("細節相關的動畫資產"), Tooltip("玩家的眨眼動畫。"), SerializeField]
        private AnimationReferenceAsset EyeBlink { get; set; }
        
        [field: Tooltip("玩家的跑步動畫。"), SerializeField]
        private AnimationReferenceAsset Sit { get; set; }
        
        
        
        // ========== { 異步協程 } ==========
        
        // 眨眼的異步協程。
        private IEnumerator EyeBlinkCoroutine { get; set; }
        
        
        
        private void Awake()
        {
            SkeletonAnimation = GetComponent<SkeletonAnimation>();
        }
        
        
        
        private void Start()
        {
            PlayWalkAnimation();
        }
        
        
        
        private void OnEnable()
        {
            EyeBlinkCoroutine = EyeBlinkProcess();
            StartCoroutine(EyeBlinkCoroutine);
        }
        
        
        
        private void OnDisable()
        {
            if (EyeBlinkCoroutine is not null)
            {
                StopCoroutine(EyeBlinkCoroutine);
                EyeBlinkCoroutine = null;
            }
        }
        
        
        
        /// <summary>
        /// 用於播放走路的動畫。
        /// </summary>
        private void PlayWalkAnimation()
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Walk, true);
        }
        
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// 用於播放坐在位子上的動畫。
        /// </summary>
        public void PlaySitAnimation()
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Sit, true);
        }
        
        
        
        /// <summary>
        /// 用於執行眨眼的異步協程。
        /// </summary>
        /// <returns></returns>
        private IEnumerator EyeBlinkProcess()
        {
            while (true)
            {
                SkeletonAnimation.AnimationState.SetAnimation(2, EyeBlink, false);
                
                var targetWaitTime = Random.Range(CustomerAttribute.EyeBlinkRange.Min, CustomerAttribute.EyeBlinkRange.Max);
                yield return new WaitForSeconds(targetWaitTime);
            }
        }
    }
}