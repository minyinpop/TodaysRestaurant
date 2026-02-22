using UnityEngine;

namespace Common.Value
{
    [System.Serializable]
    public sealed class Range
    {
        [field: SerializeField] private int min;
                                public int Min => min;
        [field: SerializeField] private int max;
                                public int Max => max;
    }
}