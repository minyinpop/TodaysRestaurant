using Data.General;
using UnityEngine;

namespace Data.Mob.Character.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Character/Friendly", fileName = "New Data")]
    internal sealed class CharacterSO : ScriptableObject
    {
        #region Health
            [field: Header("Health")]
            [field: SerializeField] private Health Health;

            public void GetHealthValues(out int min, out int max)
            {
                Health.GetValues(out min, out max);
            }
        #endregion
    }
}