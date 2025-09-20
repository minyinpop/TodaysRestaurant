using System.Collections.Generic;
using Data.General;
using Data.General.Character_Information.Base;
using UnityEngine;

namespace Data.Mob.Character.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Character/Friendly", fileName = "New Data")]
    internal sealed class CharacterSO : ScriptableObject
    {
        #region CharacterInformatin
            [field: Header("Information")]
            [field: SerializeField] private CharacterInformation CharacterInformation;
            
            #region CharacterType
                public void GetCharacterType(out CharacterType type)
                {
                    CharacterInformation.GetCharacterType(out type);
                }
            #endregion
            
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