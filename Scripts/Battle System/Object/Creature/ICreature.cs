using System.Collections.Generic;
using Common.Value;
using Common.Value.Type;

namespace Battle_System.Object.Creature
{
    public interface ICreature
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