using System;
using System.Collections.Generic;
using UnityEngine;

namespace Animation_System.Spine
{
    [Serializable]
    internal sealed class SpineChainAnimation
    {
        [field: SerializeField] private List<SpineAnimation> Animations = new();

        public void GetValues(out List<SpineAnimation> animations)
        {
            animations = Animations;
        }
    }
}