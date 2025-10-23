using System.Collections.Generic;
using Data.General;
using Data.General.Enum;
using Data.Mob.Character.Main;
using UnityEngine;

namespace Data.Mob.Character.Child.Avatar.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Avatar", fileName = "New Data")]
    internal sealed class AvatarSO : CharacterSO
    {
        #region Battle
            [field: Header("Battle Card Type")]
            [field: SerializeField] private List<CardType> UseCardType;
            public override void GetUseCardType(out List<CardType> cardType) { cardType = UseCardType; }
        #endregion

        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private Health Health;
            public override void GetHealth(out int min, out int max) { Health.GetValues(out min, out max); }
        #endregion
    }
}