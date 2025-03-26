using Spine.Unity;
using UnityEngine;

namespace NPC.Customer
{
    /// <summary>
    /// 用來管理顧客的動畫的類。
    /// 需要 CustomerManager 這個類的支援。
    /// </summary>
    [RequireComponent(typeof(SkeletonAnimation))]
    [RequireComponent(typeof(CustomerManager))]
    public class CustomerAnimation : MonoBehaviour
    {
        [Header("動畫資產"), Tooltip("Spine 插件的坐椅子動畫。"), SerializeField]
        private AnimationReferenceAsset seat;
        
        // 用來當作播放玩家動畫的組件。
        private SkeletonAnimation _skeletonAnimation;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        /// <summary>
        /// 播放坐椅子的動畫。
        /// </summary>
        public void Seat()
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, seat, true);
        }
    }
}
