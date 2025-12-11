using Economy_System.Child.Creature.Customer.Data;
using Spine.Unity;
using UnityEngine;

namespace Economy_System.Child.Creature.Customer.System.Child
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