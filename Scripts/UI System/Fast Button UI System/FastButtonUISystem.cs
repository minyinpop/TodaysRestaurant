using System;
using Common.Button;
using UnityEngine;

namespace UI_System.Fast_Button_UI_System
{
    public sealed class FastButtonUISystem : MonoBehaviour
    {
        [field: Header("快捷按鈕")]
        [field: SerializeField] private Button backpackFastButton;
                                public event Action OnClickBackpackFastButton;
        
        [field: SerializeField] private Button levelSelectFastButton;
                                public event Action OnClickLevelSelectFastButton;
        
        [field: SerializeField] private Button settingsFastButton;
                                public event Action OnClickSettingsFastButton;
        
        [field: SerializeField] private Button TutorialFastButton;
                                public event Action OnClickTutorialFastButton;
        
        
        private void Awake()
        {
            if (backpackFastButton is not null)
                backpackFastButton.OnClick += HandleBackpackFastButton;
            
            if (levelSelectFastButton is not null)
                levelSelectFastButton.OnClick += HandleLevelSelectFastButton;
            
            if (settingsFastButton is not null)
                settingsFastButton.OnClick += HandleSettingsFastButton;

            if (TutorialFastButton is not null)
                TutorialFastButton.OnClick += HandleTutorialFastButton;
        }

        private void OnDestroy()
        {
            if (backpackFastButton is not null)
                backpackFastButton.OnClick -= HandleBackpackFastButton;
            
            if (levelSelectFastButton is not null)
                levelSelectFastButton.OnClick -= HandleLevelSelectFastButton;
            
            if (settingsFastButton is not null)
                settingsFastButton.OnClick -= HandleSettingsFastButton;
            
            if (TutorialFastButton is not null)
                TutorialFastButton.OnClick -= HandleTutorialFastButton;
        }

        private void HandleBackpackFastButton()
        {
            if (OnClickBackpackFastButton is null)
            {
                Debug.Log($"{nameof(OnClickBackpackFastButton)} 沒有被其它 class 訂閱。");
                return;
            }

            OnClickBackpackFastButton.Invoke();
        }
        
        private void HandleLevelSelectFastButton()
        {
            if (OnClickLevelSelectFastButton is null)
            {
                Debug.Log($"{nameof(OnClickLevelSelectFastButton)} 沒有被其它 class 訂閱。");
                return;
            }
            
            OnClickLevelSelectFastButton.Invoke();
        }
        
        private void HandleSettingsFastButton()
        {
            if (OnClickSettingsFastButton is null)
            {
                Debug.Log($"{nameof(OnClickSettingsFastButton)} 沒有被其它 class 訂閱。");
                return;
            }
            
            OnClickSettingsFastButton.Invoke();
        }

        private void HandleTutorialFastButton()
        {
            if (OnClickTutorialFastButton is null)
            {
                Debug.Log($"{nameof(OnClickTutorialFastButton)} 沒有被其它 class 訂閱。");
                return;
            }
            
            OnClickTutorialFastButton.Invoke();
        }
    }
}