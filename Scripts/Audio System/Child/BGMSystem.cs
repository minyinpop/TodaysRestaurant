using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Audio_System.Data;
using UnityEngine;

namespace Audio_System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class BGMSystem : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("聲音播放器")]
        [field: SerializeField] private AudioSource BGMSource;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
            
            if (BGMSource is null)
            {
                throw new InvalidOperationException($"{nameof(BGMSource)} 沒有被掛載。");
            }
        }

        public void FadeInBGM(PlayBGMData data, Action onComplete = null)
        {
            if (data.Clip is null)
            {
                Debug.Log($"{nameof(BGMSystem)} 無法播放空的 {nameof(AudioClip)}。");
                return;
            }

            BGMSource.clip = data.Clip;

            BGMSource.loop = data.Loop;

            BGMSource.time = data.startTime;
            
            BGMSource.Play();
            
            animation.DoFade_AudioSource(
                source: BGMSource,
                settings: data.Settings,
                onComplete: () =>
                {
                    onComplete?.Invoke();
                });
        }

        public void FadeOutBGM(DoFade_AudioSource settings, Action onComplete = null)
        {
            animation.DoFade_AudioSource(
                source: BGMSource,
                settings: settings,
                onComplete: () =>
                {
                    BGMSource.Stop();
                    
                    BGMSource.clip = null;
                    
                    BGMSource.loop = false;

                    BGMSource.time = 0;
                    
                    onComplete?.Invoke();
                });
        }
    }
}