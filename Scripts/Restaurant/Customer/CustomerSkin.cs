using System.Collections.Generic;
using Database.Restaurant.Customer.Skin;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Restaurant.Customer
{
    internal class CustomerSkin : MonoBehaviour
    {
        private Skeleton Skeleton { get; set; }
        private SkeletonData SkeletonData { get; set; }
        private Skin Skin { get; set; } = new("Customer");

        private void Awake()
        {
            Skeleton = GetComponent<SkeletonAnimation>().Skeleton;
            SkeletonData = Skeleton.Data;
        }

        public void Init(List<SkinSO> skins)
        {
            foreach (var skin in skins)
                Skin.AddSkin(SkeletonData.FindSkin(skin.GetRandomSkin()));
            
            Skeleton.SetSkin(Skin);
            Skeleton.SetSlotsToSetupPose();
        }
    }
}