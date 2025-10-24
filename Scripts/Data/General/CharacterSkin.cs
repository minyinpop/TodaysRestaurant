using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.General
{
    [Serializable]
    internal sealed class CharacterSkin
    {
        [field: Header("Name")]
        [field: SerializeField] private string SkinTypeName;
        [field: SerializeField] private List<string> SkinNames;
        public void GetRandomSkin(out string skinName) { skinName = $"{SkinTypeName}/{SkinNames[Random.Range(0, SkinNames.Count)]}"; }
    }
}