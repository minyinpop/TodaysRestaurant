using System.Collections.Generic;
using Data.General;
using Data.General.Enum;

namespace Data.Battle_System.Creature.Interface
{
    public interface IBattleCreature
    {
        #region Battle
            public void GetUseCardType(out List<CardType> cardType);
        #endregion

        #region Attribute
            public void GetHealth(out int min, out int max);
            public void GetDamage(out Damage damage);
        #endregion
    }
}