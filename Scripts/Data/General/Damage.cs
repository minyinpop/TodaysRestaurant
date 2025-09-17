using System;
using UnityEngine;

namespace Data.General
{
    [Serializable]
    internal class Damage
    {
        [field: Header("Values")]
        [field: SerializeField] private int BasicDamage;

        public void GetValues(out int basicDamage)
        {
            basicDamage = BasicDamage;
        }
    }
}