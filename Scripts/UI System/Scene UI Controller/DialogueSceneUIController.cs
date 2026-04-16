using UI_System.Fast_Button_UI_System;
using UI_System.Settings_UI_System.Main;
using UnityEngine;

namespace UI_System.Scene_UI_Controller
{
    public sealed class DialogueSceneUIController : MonoBehaviour
    {
        [field: Header("主系統")]
        [field: SerializeField] private FastButtonUISystem fastButtonUISystem;
        
        [field: Header("介面")]
        [field: SerializeField] private SettingsUISystem settingsUISystem;
        
        private void Awake()
        {
            fastButtonUISystem.OnClickSettingsFastButton += settingsUISystem.OpenUI;
        }

        private void OnDestroy()
        {
            fastButtonUISystem.OnClickSettingsFastButton -= settingsUISystem.OpenUI;
        }
    }
}