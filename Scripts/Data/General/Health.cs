using System;
using UnityEngine;

namespace Data.General
{
    [Serializable]
    internal class Health
    {
        [field: SerializeField] private int MaxHealth;
        public int CurrentHealth;
    }
}