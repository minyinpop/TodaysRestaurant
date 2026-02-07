using UnityEngine;

namespace Common.Value
{
    [System.Serializable]
    public class Health
    {
        [field: Header("Values")]
        [field: SerializeField] private int max;
                                public int Max => max;
        [field: SerializeField] private int min;
                                public int Min => min;
    }
}