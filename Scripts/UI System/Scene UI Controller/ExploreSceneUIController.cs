using UI_System.Fast_Button_UI_System;
using UI_System.Player_UI_System.Child.Backpack_UI_System.System;
using UI_System.Settings_UI_System.Main;
using UI_System.Tutorial_UI_System.System;
using UnityEngine;

namespace UI_System.Scene_UI_Controller
{
    public sealed class ExploreSceneUIController : MonoBehaviour
    {
        [field: Header("主系統")]
        [field: SerializeField] private FastButtonUISystem fastButtonUISystem;
        
        [field: Header("介面")]
        [field: SerializeField] private BackpackUISystem backpackUISystem;
        [field: SerializeField] private SettingsUISystem settingsUISystem;
        [field: SerializeField] private TutorialUISystem tutorialUISystem;

        private void Awake()
        {
            fastButtonUISystem.OnClickBackpackFastButton += backpackUISystem.SetBackpackUI;
            fastButtonUISystem.OnClickSettingsFastButton += settingsUISystem.OpenUI;
            fastButtonUISystem.OnClickTutorialFastButton += tutorialUISystem.OpenUI;
        }
        
        private void OnDestroy()
        {
            fastButtonUISystem.OnClickBackpackFastButton -= backpackUISystem.SetBackpackUI;
            fastButtonUISystem.OnClickSettingsFastButton -= settingsUISystem.OpenUI;
            fastButtonUISystem.OnClickTutorialFastButton -= tutorialUISystem.OpenUI;
        }
    }
}