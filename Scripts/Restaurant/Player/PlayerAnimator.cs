using System.Collections;
using Database.Player.Attribute;
using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Restaurant.Player
{
    // ==================================================
    // 用來控制玩家動畫的程式碼。
    // 使用 Spine 系統來顯示玩家動畫。
    // ==================================================
    
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(SkeletonAnimation))]
    public class PlayerAnimator : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        [field: Header("玩家屬性資料"), SerializeField]
        public PlayerAttributeSO PlayerAttribute { get; private set; }
        
        
        
        // ========== { 自身組件 } ==========
        
        // 自身的 SkeletonAnimation 組件。
        private SkeletonAnimation SkeletonAnimation { get; set; }
        
        
        
        // ========== { 移動相關的動畫資產 } ==========
        
        [field: Header("移動相關的動畫資產"), Tooltip("玩家的閒置動畫。"), SerializeField]
        private AnimationReferenceAsset Idle { get; set; }
        
        [field: Tooltip("玩家的移動動畫。"), SerializeField]
        private AnimationReferenceAsset Move { get; set; }
        
        [field: Tooltip("玩家的跑步動畫。"), SerializeField]
        private AnimationReferenceAsset Run { get; set; }
        
        
        
        // ========== { 細節相關的動畫資產 } ==========
        
        [field: Header("細節相關的動畫資產"), Tooltip("玩家的眨眼動畫。"), SerializeField]
        private AnimationReferenceAsset EyeBlink { get; set; }
        
        
        
        // ========== { 異步協程 } ==========
        
        // 眨眼的異步協程。
        private IEnumerator EyeBlinkCoroutine { get; set; }



        private void Awake()
        {
            SkeletonAnimation = GetComponent<SkeletonAnimation>();
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
        /// 用於切換到玩家閒置的動畫。
        /// </summary>
        private void PlayIdleAnimation()
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Idle, true);
        }
        
        
        
        /// <summary>
        /// 用於切換到玩家移動的動畫。
        /// </summary>
        private void PlayWalkAnimation()
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Move, true);
        }

        
        
        /// <summary>
        /// 用於切換到玩家跑步的動畫。
        /// </summary>
        private void PlayRunAnimation()
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Run, true);
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
                
                var targetWaitTime = Random.Range(PlayerAttribute.EyeBlinkRange.Min, PlayerAttribute.EyeBlinkRange.Max);
                yield return new WaitForSeconds(targetWaitTime);
            }
        }
    }
}