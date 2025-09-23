using System;
using UnityEngine;

namespace Data.General
{
    [Serializable]
    internal sealed class Range
    {
        [field: SerializeField] private int Min;
        [field: SerializeField] private int Max;

        public void GetValues(out int min, out int max)
        {
            min = Min;
            max = Max;
        }
    }
}