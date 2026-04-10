using System;
using Animation_System.DOTween.Basic;
using UnityEngine;

namespace Audio_System.Data
{
    [Serializable]
    public sealed class PlayBGMData
    {
        public AudioClip Clip;

        public bool Loop;

        public float startTime;
        
        public DoFade_AudioSource Settings;
    }
}