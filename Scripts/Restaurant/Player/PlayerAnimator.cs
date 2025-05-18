using System.Collections;
using Database.Restaurant.Player.Attribute;
using Spine.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using InputSystem = Input.InputSystem;

namespace Restaurant.Player
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class PlayerAnimator : MonoBehaviour
    {
        [field: FormerlySerializedAs("<PlayerAttribute>k__BackingField")]
        [field: Header("玩家的屬性資料")]
        [field: SerializeField] private AttributeSO Attribute { get; set; }
        
        [field: Header("主要的 Spine 動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset Idle { get; set; }
        [field: SerializeField] private AnimationReferenceAsset Walk { get; set; }
        
        [field: Header("次要的 Spine 動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset Blink { get; set; }
        
        private InputManager Input { get; set; }
        private Vector3 MoveDir => Input.Player.Walk.ReadValue<Vector3>();
        
        private SkeletonAnimation SkeletonAnimation { get; set; }
        
        private IEnumerator BlinkCoroutine { get; set; }

        private void Awake()
        {
            Input = InputSystem.Input;
            
            SkeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void Start()
        {
            BlinkCoroutine = BlinkProcess();
            StartCoroutine(BlinkCoroutine);
        }

        private void OnEnable()
        {
            Input.Player.Walk.started += OnWalk;
            Input.Player.Walk.canceled += OnIdle;
        }
        
        private void OnDisable()
        {
            Input.Player.Walk.started -= OnWalk;
            Input.Player.Walk.canceled -= OnIdle;

            if (BlinkCoroutine is not null)
            {
                StopCoroutine(BlinkCoroutine);
                BlinkCoroutine = null;
            }
        }

        private void Update()
        {
            if (MoveDir.x > 0)
                SkeletonAnimation.Skeleton.ScaleX = -1;
            else if (MoveDir.x < 0)
                SkeletonAnimation.Skeleton.ScaleX = 1;
        }

        private void OnIdle(InputAction.CallbackContext context)
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Idle, true);
        }
        
        private void OnWalk(InputAction.CallbackContext context)
        {
            SkeletonAnimation.AnimationState.SetAnimation(1, Walk, true);
        }

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