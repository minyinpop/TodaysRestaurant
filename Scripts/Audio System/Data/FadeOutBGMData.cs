using System;
using Animation_System.DOTween.Basic;

namespace Audio_System.Data
{
    [Serializable]
    public sealed class FadeOutBGMData
    {
        public bool KeepClip;

        public DoFade_AudioSource Settings;
    }
}