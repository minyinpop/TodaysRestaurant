using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Animation.Spine
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