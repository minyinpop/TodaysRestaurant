using System;
using Animation_System.DOTween;
using Audio_System.Data;
using UnityEngine;

namespace Audio_System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class SFXSystem : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("聲音播放器")]
        [field: SerializeField] private AudioSource SFXSource;
        
        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
            
            if (SFXSource is null)
            {
                throw new InvalidOperationException($"{nameof(SFXSource)} 沒有被掛載。");
            }
        }
        
        public void PlaySFX(PlaySFXData data)
        {
            if (data.Clip is null)
            {
                Debug.Log($"{nameof(SFXSystem)} 無法播放空的 {nameof(AudioClip)}。");
                return;
            }
            
            SFXSource.PlayOneShot(data.Clip);
        }
    }
}