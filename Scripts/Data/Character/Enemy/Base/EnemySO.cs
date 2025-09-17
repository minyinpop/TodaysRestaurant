using Data.General;
using UnityEngine;

namespace Data.Character.Enemy.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Character/Enemy", fileName = "New Data")]
    internal sealed class EnemySO : ScriptableObject
    {
        #region Health
            [field: Header("Health")]
            [field: SerializeField] private Health Health;

            public void GetHealthValues(out int min, out int max)
            {
                Health.GetValues(out min, out max);
            }
        #endregion

        #region Damage
            [field: Header("Damage")]
            [field: SerializeField] private Damage Damage;

            public void GetDamageValues(out int basicDamage)
            {
                Damage.GetValues(out basicDamage);
            }
        #endregion
    }
}