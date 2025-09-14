using System;
using UnityEngine;

namespace Data.Attribute
{
    [Serializable]
    internal sealed class SkeletonAnimationValue
    {
        [field: SerializeField] private int Layer;
        [field: SerializeField] private string AnimationName;
        [field: SerializeField] private bool Loop;
        
        public void GetValues(out int layer, out string animation, out bool loop)
        {
            layer = Layer;
            animation = AnimationName;
            loop = Loop;
        }
    }
}