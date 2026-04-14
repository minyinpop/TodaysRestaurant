using System;

namespace Common.Data_Saver.Player_Settings_Saver.Child
{
    [Serializable]
    public sealed class SettingsSaveData
    {
        public float MasterVolume;
        
        public float MainBGMVolume;
        public float CommonBGMVolume;
        
        public float MainSFXVolume;
        public float UISFXVolume;
        public float FootstepVolume;
        public float InteractSFXVolume;
        public float AttackSFXVolume;
        public float OtherSFXVolume;

        public float MainAMBVolume;
        public float RestaurantAMBVolume;
    }
}