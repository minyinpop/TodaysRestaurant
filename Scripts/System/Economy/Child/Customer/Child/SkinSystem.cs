using Data.Mob.Character.Child.Customer.Base;
using Spine.Unity;
using UnityEngine;

namespace System.Economy.Child.Customer.Child
{
    internal sealed class SkinSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Data")]
        [field: SerializeField] private CustomerSO CustomerData;

        public void SetRandomSkin()
        {
            var skeleton = SkeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            CustomerData.GetRandomSkin(skeletonData, out var skin);
            skeleton.SetSkin(skin);
            skeleton.SetSlotsToSetupPose();
        }
    }
}