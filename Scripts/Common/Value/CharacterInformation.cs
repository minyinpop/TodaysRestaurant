using System;
using System.Collections.Generic;
using Common.Value.Type;
using UnityEngine;

namespace Common.Value
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