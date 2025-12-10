using System.Collections.Generic;
using Data.Battle_System.Creature.Battle.Interface;
using Data.General;
using Data.General.Enum;
using UnityEngine;

namespace Data.Battle_System.Creature.Battle.Data.Enemy.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Enemy", fileName = "New Data")]
    internal sealed class EnemySO : ScriptableObject, IBattleCreature
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