using UI_System.Fast_Button_UI_System;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.System;
using UI_System.Player_UI_System.Child.Backpack_UI_System.System;
using UI_System.Settings_UI_System.Main;
using UnityEngine;

namespace UI_System.Scene_UI_Controller
{
    public sealed class SceneUIController : MonoBehaviour
    {
        [field: Header("主系統")]
        [field: SerializeField] private FastButtonUISystem fastButtonUISystem;
        
        [field: Header("介面")]
        [field: SerializeField] private LevelSelectUISystem levelSelectUISystem;
        [field: SerializeField] private BackpackUISystem backpackUISystem;
        [field: SerializeField] private SettingsUISystem settingsUISystem;
        
        private void Awake()
        {
            if (levelSelectUISystem is not null)
                fastButtonUISystem.OnClickLevelSelectFastButton += levelSelectUISystem.ShowLevelSelectUI;

            if (backpackUISystem is not null)
                fastButtonUISystem.OnClickBackpackFastButton += backpackUISystem.SetBackpackUI;
            
            if (fastButtonUISystem is not null)
                fastButtonUISystem.OnClickSettingsFastButton += settingsUISystem.OpenUI;
        }

        private void OnDestroy()
        {
            if (levelSelectUISystem is not null)
                fastButtonUISystem.OnClickLevelSelectFastButton -= levelSelectUISystem.ShowLevelSelectUI;

            if (backpackUISystem is not null)
                fastButtonUISystem.OnClickBackpackFastButton -= backpackUISystem.SetBackpackUI;
            
            if (fastButtonUISystem is not null)
                fastButtonUISystem.OnClickSettingsFastButton -= settingsUISystem.OpenUI;
        }
    }
}