using System;
using Audio_System.Child;
using UnityEngine;

namespace Audio_System.Main
{
    public sealed class AudioSystem : MonoBehaviour
    {
        [field: Header("自身系統")]
        [field: SerializeField] private BGMSystem bgmSystem;
                                public BGMSystem BGMSystem => bgmSystem;
        [field: SerializeField] private SFXSystem sfxSystem;
                                public SFXSystem SFXSystem => sfxSystem;
        
        public static AudioSystem Instance { get; private set; }
        
        private void Awake()
        {
            if (bgmSystem is null)
            {
                throw new InvalidOperationException($"{nameof(bgmSystem)} 沒有被掛載。");
            }
            
            if (sfxSystem is null)
            {
                throw new InvalidOperationException($"{nameof(sfxSystem)} 沒有被掛載。");
            }
            
            if (Instance is not null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}