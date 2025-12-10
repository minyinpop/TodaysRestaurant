using System;
using Data.General.Enum;
using UnityEngine;

namespace Data.General
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