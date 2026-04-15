using System;
using UnityEngine;

namespace Audio_System.Data
{
    [Serializable]
    public sealed class PlaySFXData
    {
        public AudioSourceType AudioSourceType;
        
        public AudioClip Clip;
    }
}