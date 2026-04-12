using System;
using Audio_System.Child;
using UnityEngine;

namespace Audio_System.Main
{
    public sealed class AudioSystem : MonoBehaviour
    {
        [field: Header("自身系統 - 音樂")]
        [field: SerializeField] private BGMSystem commonBGM;
                                public BGMSystem CommonBGM => commonBGM;
        
        [field: Header("自身系統 - 音效")]
        [field: SerializeField] private SFXSystem uiSFX;
                                public SFXSystem UISFX => uiSFX;
        [field: SerializeField] private SFXSystem footstepSfx;
                                public SFXSystem FootstepSFX => footstepSfx;
        [field: SerializeField] private SFXSystem interactSFX;
                                public SFXSystem InteractSFX => interactSFX;
        [field: SerializeField] private SFXSystem otherSFX;
                                public SFXSystem OtherSFX => otherSFX;
        
        public static AudioSystem Instance { get; private set; }
        
        private void Awake()
        {
            if (commonBGM is null)
            {
                throw new InvalidOperationException($"{nameof(commonBGM)} 沒有被掛載。");
            }

            if (uiSFX is null)
            {
                throw new InvalidOperationException($"{nameof(uiSFX)} 沒有被掛載。");
            }
            
            if (footstepSfx is null)
            {
                throw new InvalidOperationException($"{nameof(footstepSfx)} 沒有被掛載。");
            }
            
            if (interactSFX is null)
            {
                throw new InvalidOperationException($"{nameof(interactSFX)} 沒有被掛載。");
            }

            if (otherSFX is null)
            {
                throw new InvalidOperationException($"{nameof(otherSFX)} 沒有被掛載。");
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