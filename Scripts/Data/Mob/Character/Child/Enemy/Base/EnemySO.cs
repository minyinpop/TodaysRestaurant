using Data.General;
using Data.Mob.Character.Main;
using UnityEngine;

namespace Data.Mob.Character.Child.Enemy.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Enemy", fileName = "New Data")]
    internal sealed class EnemySO : CharacterSO
    {
        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private Health Health;
            [field: SerializeField] private Damage Damage;
            
            public override void GetHealth(out int min, out int max) { Health.GetValues(out min, out max); }
            public override void GetDamage(out Damage damage) { damage = Damage; }
        #endregion
    }
}