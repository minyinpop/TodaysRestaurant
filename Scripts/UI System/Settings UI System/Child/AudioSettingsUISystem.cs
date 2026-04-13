using System;
using System.Collections.Generic;
using Common.Data_Saver.Player_Settings_Saver.Child;
using Common.Data_Saver.Player_Settings_Saver.Main;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI_System.Settings_UI_System.Child
{
    public sealed class AudioSettingsUISystem : MonoBehaviour
    {
        [field: Header("主音量")]
        [field: SerializeField] private Slider masterVolumeSlider;
        
        [field: Header("音樂音量")]
        [field: SerializeField] private Slider mainBGMVolumeSlider;
        [field: SerializeField] private Slider commonBGMVolumeSlider;
        
        [field: Header("音效音量")]
        [field: SerializeField] private Slider mainSFXVolumeSlider;
        [field: SerializeField] private Slider uiSFXVolumeSlider;
        [field: SerializeField] private Slider footStepSFXVolumeSlider;
        [field: SerializeField] private Slider interactSFXVolumeSlider;
        [field: SerializeField] private Slider otherSFXVolumeSlider;
        
        [field: Header("環境音音量")]
        [field: SerializeField] private Slider mainAMBVolumeSlider;
        [field: SerializeField] private Slider restaurantAMBVolumeSlider;
        
        [field: Header("混音組件")]
        [field: SerializeField] private AudioMixer audioMixer;
        
        private readonly Dictionary<Slider, UnityAction<float>> _listeners = new();

        private bool _initialized;

        private void Awake()
        {
            if (masterVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(masterVolumeSlider)} 沒有被掛載。");
            }
            
            if (mainBGMVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(mainBGMVolumeSlider)} 沒有被掛載。");
            }
            
            if (mainSFXVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(mainSFXVolumeSlider)} 沒有被掛載。");
            }
            
            if (uiSFXVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(uiSFXVolumeSlider)} 沒有被掛載。");
            }
            
            if (footStepSFXVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(footStepSFXVolumeSlider)} 沒有被掛載。");
            }
            
            if (interactSFXVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(interactSFXVolumeSlider)} 沒有被掛載。");
            }
            
            if (commonBGMVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(commonBGMVolumeSlider)} 沒有被掛載。");
            }
            
            if (otherSFXVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(otherSFXVolumeSlider)} 沒有被掛載。");
            }

            if (mainAMBVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(mainAMBVolumeSlider)} 沒有被掛載。");
            }
            
            if (restaurantAMBVolumeSlider is null)
            {
                throw new InvalidOperationException($"{nameof(restaurantAMBVolumeSlider)} 沒有被掛載。");
            }

            if (audioMixer is null)
            {
                throw new InvalidOperationException($"{nameof(audioMixer)} 沒有被掛載。");
            }
        }

        private void OnDisable()
        {
            PlayerSettingsSaver.SaveSettingsToLocal(new SettingsSaveData
            {
                MasterVolume = masterVolumeSlider.value,
                
                MainBGMVolume = mainBGMVolumeSlider.value,
                CommonBGMVolume = commonBGMVolumeSlider.value,
                
                MainSFXVolume = mainSFXVolumeSlider.value,
                UISFXVolume = uiSFXVolumeSlider.value,
                FootstepVolume = footStepSFXVolumeSlider.value,
                InteractSFXVolume = interactSFXVolumeSlider.value,
                OtherSFXVolume = otherSFXVolumeSlider.value,
                
                MainAMBVolume = mainAMBVolumeSlider.value,
                RestaurantAMBVolume = restaurantAMBVolumeSlider.value,
            });
        }

        private void OnDestroy()
        {
            foreach (var listener in _listeners)
            {
                listener.Key.onValueChanged.RemoveListener(listener.Value);
            }
            
            _listeners.Clear();
        }

        public void Initialize()
        {
            if (_initialized)
            {
                Debug.Log($"{nameof(AudioSettingsUISystem)} 已經初始化過了。");
                return;
            }
            
            _initialized = true;
            
            PlayerSettingsSaver.LoadSettingsFromLocal(out var saveData);
            
            masterVolumeSlider.SetValueWithoutNotify(saveData.MasterVolume);
            
            mainBGMVolumeSlider.SetValueWithoutNotify(saveData.MainBGMVolume);
            commonBGMVolumeSlider.SetValueWithoutNotify(saveData.CommonBGMVolume);
            
            mainSFXVolumeSlider.SetValueWithoutNotify(saveData.MainSFXVolume);
            uiSFXVolumeSlider.SetValueWithoutNotify(saveData.UISFXVolume);
            footStepSFXVolumeSlider.SetValueWithoutNotify(saveData.FootstepVolume);
            interactSFXVolumeSlider.SetValueWithoutNotify(saveData.InteractSFXVolume);
            otherSFXVolumeSlider.SetValueWithoutNotify(saveData.OtherSFXVolume);
            
            mainAMBVolumeSlider.SetValueWithoutNotify(saveData.MainAMBVolume);
            restaurantAMBVolumeSlider.SetValueWithoutNotify(saveData.RestaurantAMBVolume);
            
            RegisterListener(masterVolumeSlider, "Master Volume");
            
            RegisterListener(mainBGMVolumeSlider, "Main BGM Volume");
            RegisterListener(commonBGMVolumeSlider, "Common BGM Volume");
            
            RegisterListener(mainSFXVolumeSlider, "Main SFX Volume");
            RegisterListener(uiSFXVolumeSlider, "UI SFX Volume");
            RegisterListener(footStepSFXVolumeSlider, "Footstep SFX Volume");
            RegisterListener(interactSFXVolumeSlider, "Interact SFX Volume");
            RegisterListener(otherSFXVolumeSlider, "Other SFX Volume");
            
            RegisterListener(mainAMBVolumeSlider, "Main AMB Volume");
            RegisterListener(restaurantAMBVolumeSlider, "Restaurant AMB Volume");
        }
        
        private void RegisterListener(Slider slider, string valueName)
        {
            #region 初始化設定混音器
                audioMixer.SetFloat(valueName, slider.value);
            #endregion
            
            #region 註冊事件
                UnityAction<float> action = sliderValue => SetVolume(slider, valueName, sliderValue);
                
                slider.onValueChanged.AddListener(action);
                
                _listeners.Add(slider, action);
            #endregion
        }

        private void SetVolume(Slider slider, string valueName, float sliderValue)
        {
            slider.value = sliderValue;
            
            audioMixer.SetFloat(valueName, sliderValue);
        }
    }
}