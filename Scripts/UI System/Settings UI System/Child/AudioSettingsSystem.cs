using System;
using Audio_System.Data;
using Audio_System.Main;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI_System.Settings_UI_System.Child
{
    public sealed class AudioSettingsSystem : MonoBehaviour
    {
        [field: Header("音量設定")]
        [field: SerializeField] private Slider mainVolumeSlider;
        [field: SerializeField] private Slider BGMVolumeSlider;
        [field: SerializeField] private Slider SFXVolumeSlider;
        
        [field: Header("混音組件")]
        [field: SerializeField] private AudioMixer audioMixer;

        [field: Header("聲音變化試聽音檔")]
        [field: SerializeField] private AudioClip volumeChangedClip;

        private void Awake()
        {
            if (mainVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(mainVolumeSlider)} 沒有被掛載。");
            }
            
            if (BGMVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(BGMVolumeSlider)} 沒有被掛載。");
            }
            
            if (SFXVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(SFXVolumeSlider)} 沒有被掛載。");
            }

            if (audioMixer is null)
            {
                throw new InvalidOperationException($"{nameof(audioMixer)} 沒有被掛載。");
            }

            if (volumeChangedClip is null)
            {
                throw new InvalidOperationException($"{nameof(volumeChangedClip)} 沒有被掛載。");
            }

            mainVolumeSlider.onValueChanged.AddListener(OnMainVolumeSliderChanged);
            BGMVolumeSlider.onValueChanged.AddListener(OnBGMVolumeSliderChanged);
            SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        }

        private void OnDestroy()
        {
            mainVolumeSlider.onValueChanged.RemoveListener(OnMainVolumeSliderChanged);
            BGMVolumeSlider.onValueChanged.RemoveListener(OnBGMVolumeSliderChanged);
            SFXVolumeSlider.onValueChanged.RemoveListener(OnSFXVolumeSliderChanged);
        }

        private void OnMainVolumeSliderChanged(float value)
        {
            audioMixer.SetFloat("Master Volume", value);
        }
        
        private void OnBGMVolumeSliderChanged(float value)
        {
            audioMixer.SetFloat("BGM Volume", value);
            
            AudioSystem.Instance.BGMSystem.PlayOneShot(new PlayBGMData
            {
                Clip = volumeChangedClip
            });
        }

        private void OnSFXVolumeSliderChanged(float value)
        {
            audioMixer.SetFloat("SFX Volume", value);
            
            AudioSystem.Instance.SFXSystem.PlayOneShot(new PlaySFXData
            {
                Clip = volumeChangedClip
            });
        }
    }
}