using System;
using System.Collections.Generic;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Battle_System.Object.Creature
{
    public abstract class Creature : MonoBehaviour
    {
        #region Battle
            public virtual void GetUseCardType(out List<CardType> cardType) =>
                throw new NotImplementedException();
        #endregion

        #region Attribute
            public virtual void GetHealth(out int min, out int max) =>
                throw new NotImplementedException();
            public virtual void GetDamage(out Damage damage) =>
                throw new NotImplementedException();
        #endregion
    }
}