using System;
using UnityEngine;

namespace Data.Attribute
{
    [Serializable]
    internal sealed class ChanceValue
    {
        [field: SerializeField, Range(0, 100)] private float DrawChance;
        
        public void GetDrawChance(out float chance)
        {
            chance = DrawChance;
        }
    }
}