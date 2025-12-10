using System.Collections.Generic;
using Data.Battle_System.Creature.Battle.Interface;
using Data.General;
using Data.General.Enum;
using UnityEngine;

namespace Data.Battle_System.Creature.Battle.Data.Avatar.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Avatar", fileName = "New Data")]
    internal sealed class AvatarSO : ScriptableObject, IBattleCreature
    {
        #region Battle
            [field: Header("Battle Card Type")]
            [field: SerializeField] private List<CardType> UseCardType;
            public void GetUseCardType(out List<CardType> cardType) { cardType = UseCardType; }
        #endregion

        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private Health Health;
            [field: SerializeField] private Damage Damage;

            public void GetHealth(out int min, out int max) { Health.GetValues(out min, out max); }
            public void GetDamage(out Damage damage) { damage = null; }
        #endregion
    }
}