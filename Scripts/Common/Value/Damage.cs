using Common.Value.Type;
using UnityEngine;

namespace Common.Value
{
    [System.Serializable]
    public class Damage
    {
        [field: Header("Values")]
        [field: SerializeField] private AttackType attackType;
                                public AttackType AttackType => attackType;
        [field: SerializeField] private int basicDamage;
                                public int BasicDamage => basicDamage;
    }
}