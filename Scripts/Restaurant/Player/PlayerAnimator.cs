using System.Collections;
using Database.Player.Attribute;
using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

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
        
        [field: Tooltip("玩家的走路動畫。"), SerializeField]
        private AnimationReferenceAsset Walk { get; set; }
        
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
            InputSystem.Input.Player.Walk.started += PlayWalkAnimation;
            InputSystem.Input.Player.Walk.canceled += PlayIdleAnimation;
            
            InputSystem.Input.Player.Run.started += PlayRunAnimation;
            InputSystem.Input.Player.Run.canceled += PlayIdleOrWalkAnimation;
            
            EyeBlinkCoroutine = EyeBlinkProcess();
            StartCoroutine(EyeBlinkCoroutine);
        }
        
        
        
        private void OnDisable()
        {
            InputSystem.Input.Player.Walk.started -= PlayWalkAnimation;
            InputSystem.Input.Player.Walk.canceled -= PlayIdleAnimation;
            
            InputSystem.Input.Player.Run.started -= PlayRunAnimation;
            InputSystem.Input.Player.Run.canceled -= PlayIdleOrWalkAnimation;
            
            if (EyeBlinkCoroutine is not null)
            {
                StopCoroutine(EyeBlinkCoroutine);
                EyeBlinkCoroutine = null;
            }
        }

        
        
        private void Update()
        {
            if (InputSystem.MoveDirection().x > 0)
                SkeletonAnimation.Skeleton.ScaleX = -1;
            else if (InputSystem.MoveDirection().x < 0)
                SkeletonAnimation.Skeleton.ScaleX = 1;
        }

        
        
        /// <summary>
        /// 用於播放閒置動畫的方法。
        /// </summary>
        private void PlayIdleAnimation(InputAction.CallbackContext context)
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Idle, true);
        }
        
        
        
        /// <summary>
        /// 用於播放跑步或走路動畫的方法。
        /// 判斷玩家是否再開始走路前，就按下了跑步鍵。
        /// </summary>
        private void PlayWalkAnimation(InputAction.CallbackContext context)
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, PlayerAttribute.Run.IsRunning ? Run : Walk, true);
        }

        
        
        /// <summary>
        /// 用來播放跑步動畫的方法。
        /// 當在走路的狀態，判斷玩家是否按下跑步鍵。
        /// </summary>
        private void PlayRunAnimation(InputAction.CallbackContext context)
        {
            if (PlayerAttribute.Walk.IsRunning)
                SkeletonAnimation.AnimationState.SetAnimation(1, Run, true);
        }

        
        
        /// <summary>
        /// 用於播放走路或閒置動畫的方法。
        /// 取消跑步後，判斷玩家目前是不動還是走路的狀態。
        /// </summary>
        private void PlayIdleOrWalkAnimation(InputAction.CallbackContext context)
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, PlayerAttribute.Walk.IsRunning ? Walk : Idle, true);
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