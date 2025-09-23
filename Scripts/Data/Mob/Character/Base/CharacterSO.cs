using System.Collections.Generic;
using Data.General;
using Data.General.Enum;
using UnityEngine;

namespace Data.Mob.Character.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Character Data", fileName = "New Data")]
    internal sealed class CharacterSO : ScriptableObject
    {
        #region CharacterInformatin
            [field: Header("Information")]
            [field: SerializeField] private CharacterInformation CharacterInformation;
            
            #region UseCardType
                public void GetUseCardType(out List<CardType> cardType)
                {
                    CharacterInformation.GetUseCardType(out cardType);
                }
            #endregion
        #endregion
        
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