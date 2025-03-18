using Input;
using Spine.Unity;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 用來控制玩家的動畫系統的類，在此使用的是 Spine 裡的 Skeleton Animation。
    /// </summary>
    [RequireComponent(typeof(SkeletonAnimation))]
    public class PlayerAnimationSystem : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;
        public AnimationReferenceAsset walk;
        
        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }
    }
}
