using System;
using UnityEngine;

namespace Common.Value
{
    [Serializable]
    internal class Health
    {
        [field: Header("Values")]
        [field: SerializeField] private int MaxHealth;
        [field: SerializeField] private int MinHealth;

        public void GetValues(out int min, out int max)
        {
            min = MinHealth;
            max = MaxHealth;
        }
    }
}