using System;
using UnityEngine;

namespace Data.Attribute
{
    [Serializable]
    internal sealed class HealthValue
    {
        [field: SerializeField] private int MaxHealth;
        [field: SerializeField] private int CurrentHealth;
    }
}