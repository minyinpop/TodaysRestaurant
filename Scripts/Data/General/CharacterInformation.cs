using System;
using System.Collections.Generic;
using Data.General.Enum;
using UnityEngine;

namespace Data.General
{
    [Serializable]
    internal class CharacterInformation
    {
        #region UseCardType
            [field: SerializeField] private List<CardType> UseCardType;
            public void GetUseCardType(out List<CardType> cardType) { cardType = UseCardType; }
        #endregion
    }
}