using System;
using Common.Value.Type;
using UnityEngine;

namespace Common.Value
{
    [Serializable]
    public class Damage
    {
        [field: Header("Values")]
        [field: SerializeField] private AttackType AttackType;
        [field: SerializeField] private int BasicDamage;

        public void GetValues(out AttackType attackType, out int basicDamage)
        {
            attackType = AttackType;
            basicDamage = BasicDamage;
        }
    }
}