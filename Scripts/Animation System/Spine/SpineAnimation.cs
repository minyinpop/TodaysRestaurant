using System;
using UnityEngine;

namespace Animation_System.Spine
{
    [Serializable]
    public sealed class SpineAnimation
    {
        [field: SerializeField] private int Layer;
        [field: SerializeField] private string AnimationName;
        [field: SerializeField] private bool Loop;

        public SpineAnimation(int layer, string animationName, bool loop)
        {
            Layer = layer;
            AnimationName = animationName;
            Loop = loop;
        }

        public void GetValues(out int layer, out string animationName, out bool loop)
        {
            layer = Layer;
            animationName = AnimationName;
            loop = Loop;
        }
    }
}