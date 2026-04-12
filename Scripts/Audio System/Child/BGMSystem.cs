using System;
using Animation_System.DOTween;
using Audio_System.Data;
using UI_System.Message_UI_System.Main;
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

        public void PlayOneShot(FadeInBGMData data)
        {
            if (data.Clip is null)
            {
                Debug.Log($"{nameof(BGMSystem)} 無法播放空的 {nameof(AudioClip)}。");
                return;
            }
            
            BGMSource.PlayOneShot(data.Clip);
        }

        public void FadeInBGM(FadeInBGMData data, Action onComplete = null)
        {
            if (data.Clip is null)
            {
                Debug.Log($"{nameof(data)} 不能傳入空的 {nameof(AudioClip)}。");
                return;
            }
            
            MessageUISystem.ShowAudioUI(data.Clip.name);
            
            if (data.ChangeClip)
            {
                BGMSource.clip = data.Clip;
                
                BGMSource.loop = data.Loop;
                
                BGMSource.time = data.startTime;
            }
            
            BGMSource.volume = 0;
            
            BGMSource.Play();
            
            animation.DoFade_AudioSource(
                source: BGMSource,
                settings: data.Settings,
                onComplete: () =>
                {
                    onComplete?.Invoke();
                });
        }

        public void FadeOutBGM(FadeOutBGMData data, Action onComplete = null)
        {
            animation.DoFade_AudioSource(
                source: BGMSource,
                settings: data.Settings,
                onComplete: () =>
                {
                    if (data.KeepClip)
                    {
                        BGMSource.Pause();
                    }
                    else
                    {
                        BGMSource.Stop();
                        
                        BGMSource.clip = null;
                        
                        BGMSource.loop = false;
                    }
                    
                    onComplete?.Invoke();
                });
        }
    }
}