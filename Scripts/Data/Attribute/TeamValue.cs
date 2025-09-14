using System;
using UnityEngine;

namespace Data.Attribute
{
    [Serializable]
    internal sealed class TeamValue
    {
        [field: SerializeField] private int CharacterNumber;

        public void GetCharacterNumber(out int number)
        {
            number = CharacterNumber;
        }
    }
}