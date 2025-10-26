using System.Collections.Generic;
using Data.General;
using Data.General.Enum;
using UnityEngine;

namespace Data.Mob.Character.Main
{
    internal abstract class CharacterSO : ScriptableObject
    {
        #region Battle
            public virtual void GetUseCardType(out List<CardType> cardType) { cardType = null; }
        #endregion

        #region Attribute
            public virtual void GetHealth(out int min, out int max) { min = 0; max = 1; }
            public virtual void GetDamage(out Damage damage) { damage = null; }
        #endregion
    }
}