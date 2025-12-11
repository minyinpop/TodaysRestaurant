using System.Collections.Generic;
using Common.Value;
using Spine;
using UnityEngine;

namespace Restaurant_System.Object.Creature.Customer.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Customer", fileName = "New Data")]
    internal sealed class CustomerSO : ScriptableObject
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