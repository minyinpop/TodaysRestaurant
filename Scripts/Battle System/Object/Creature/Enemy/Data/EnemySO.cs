using System.Collections.Generic;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Battle_System.Object.Creature.Enemy.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Enemy", fileName = "New Data")]
    internal sealed class EnemySO : ScriptableObject, ICreature
    {
        #region Battle
            public void GetUseCardType(out List<CardType> cardType)
            {
                cardType = null;
            }
        #endregion
            
        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private Health Health;
            [field: SerializeField] private Damage Damage;

            public void GetHealth(out int min, out int max) { Health.GetValues(out min, out max); }
            public void GetDamage(out Damage damage) { damage = Damage; }
        #endregion
    }
}