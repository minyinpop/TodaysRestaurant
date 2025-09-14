using System;
using UnityEngine;

namespace Data.Attribute
{
    [Serializable]
    internal sealed class DamageValue
    {
        [field: SerializeField] private int Damage;
        
        public void GetDamage(out int damage)
        {
            damage = Damage;
        }
    }
}