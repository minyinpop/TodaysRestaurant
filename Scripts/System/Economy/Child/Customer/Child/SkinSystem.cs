using Data.Economy_System.Creature.Data.Customer.Base;
using Spine.Unity;
using UnityEngine;

namespace System.Economy.Child.Customer.Child
{
    internal sealed class SkinSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Data")]
        [field: SerializeField] private CustomerSO CreatureData;

        public void SetRandomSkin()
        {
            var skeleton = SkeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            CreatureData.GetRandomSkin(skeletonData, out var skin);
            skeleton.SetSkin(skin);
            skeleton.SetSlotsToSetupPose();
        }
    }
}