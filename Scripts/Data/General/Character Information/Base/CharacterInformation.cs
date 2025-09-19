using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.General.Character_Information.Base
{
    [Serializable]
    internal class CharacterInformation
    {
        #region CharacterType
            [field: SerializeField] private CharacterType Character;

            public void GetCharacterType(out CharacterType type)
            {
                type = Character;
            }
        #endregion
        
        #region UseCardType
            [field: SerializeField] private List<CardType> UseCardType;
            
            public void GetUseCardType(out List<CardType> cardType)
            {
                cardType = UseCardType;
            }
        #endregion
    }
}