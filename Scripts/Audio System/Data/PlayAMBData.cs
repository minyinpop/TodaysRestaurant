using System;
using UnityEngine;

namespace Audio_System.Data
{
    [Serializable]
    public sealed class PlayAMBData
    {
        public AudioSourceType AudioSourceType;
        
        public AudioClip Clip;

        public bool Loop;

        public float StartTime;
    }
}