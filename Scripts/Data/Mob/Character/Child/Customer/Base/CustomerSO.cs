using System.Collections.Generic;
using Data.General;
using Data.Mob.Character.Main;
using Spine;
using UnityEngine;

namespace Data.Mob.Character.Child.Customer.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Customer", fileName = "New Data")]
    internal sealed class CustomerSO : CharacterSO
    {
        #region Skin
            [field: Header("Skin")]
            [field: SerializeField] private List<CharacterSkin> SkinTypes;

            public void GetRandomSkin(SkeletonData skeletonData, out Skin skin)
            {
                skin = new Skin("newSkin");
                foreach (var skinType in SkinTypes)
                {
                    skinType.GetRandomSkin(out var skinName);
                    skin.AddSkin(skeletonData.FindSkin(skinName));
                }
            }
        #endregion
    }
}