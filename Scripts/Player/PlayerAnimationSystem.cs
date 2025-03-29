using System.Collections;
using Input;
using Spine.Unity;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 用來控制玩家的動畫系統的類，在此使用的是 Spine 裡的 Skeleton Animation。
    /// </summary>
    [RequireComponent(typeof(PlayerControlSystem))]
    [RequireComponent(typeof(SkeletonAnimation))]
    public class PlayerAnimationSystem : MonoBehaviour
    {
        // 用來當作播放玩家動畫的組件。
        private SkeletonAnimation _skeletonAnimation;
        
        [Header("動畫資產"), Tooltip("Spine 插件的閒置動畫。"), SerializeField]
        private AnimationReferenceAsset idle;
        
        [Tooltip("Spine 插件的走路動畫。"), SerializeField]
        private AnimationReferenceAsset walk;

        [Tooltip("Spine 插件的眨眼動畫"), SerializeField]
        private AnimationReferenceAsset eyeBlink;

        private bool _isWalk;

        private IEnumerator _eyeBlinkCoroutine;
        
        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void OnEnable()
        {
            _eyeBlinkCoroutine = A();
            StartCoroutine(_eyeBlinkCoroutine);
        }

        private void OnDisable()
        {
            StopCoroutine(_eyeBlinkCoroutine);
            _eyeBlinkCoroutine = null;
        }

        private void Update()
        {
            if (_isWalk)
            {
                if (InputSystem.PlayerMoveDirection() == Vector3.zero)
                {
                    _skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
                    _isWalk = false;
                }
            }
            else
            {
                if (InputSystem.PlayerMoveDirection() != Vector3.zero)
                {
                    _skeletonAnimation.AnimationState.SetAnimation(0, walk, true);
                    _isWalk = true;
                }
            }
        }

        private IEnumerator A()
        {
            while (true)
            {
                _skeletonAnimation.AnimationState.SetAnimation(1, eyeBlink, false);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
        }
    }
}
