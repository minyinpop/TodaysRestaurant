using System.Collections.Generic;
using Data.Battle_System.Creature.Economy.Interface;
using Data.General;
using Spine;
using UnityEngine;

namespace Data.Battle_System.Creature.Economy.Data.Customer.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Customer", fileName = "New Data")]
    internal sealed class CustomerSO : ScriptableObject, IEconomyCreature
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