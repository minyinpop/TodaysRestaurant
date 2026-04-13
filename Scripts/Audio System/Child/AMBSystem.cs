using System;
using Audio_System.Data;
using UnityEngine;

namespace Audio_System.Child
{
    public sealed class AMBSystem : MonoBehaviour
    {
        [field: Header("播放器類型")]
        [field: SerializeField] private AudioSourceType audioSourceType;
                                public AudioSourceType _audioSourceType => audioSourceType;
                                
        [field: Header("聲音播放器")]
        [field: SerializeField] private AudioSource AMBSource;

        private void Awake()
        {
            if (AMBSource is null)
            {
                throw new InvalidOperationException($"{nameof(AMBSource)} 沒有被掛載。");
            }
        }
        
        public void PlayOneShot(PlaySFXData data)
        {
            if (data.Clip is null)
            {
                Debug.Log($"{nameof(SFXSystem)} 無法播放空的 {nameof(AudioClip)}。");
                return;
            }
            
            AMBSource.PlayOneShot(data.Clip);
        }
    }
}