using System;
using System.Collections.Generic;
using Common.Button;
using Common.Level.Main;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Information_UI.Main;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Pick_UI.Main;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Main
{
    public sealed class LevelSelectUI : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private LevelPickUI levelPickUI;
        [field: SerializeField] private LevelInformationUI levelInformationUI;
        [field: SerializeField] private Button levelStartButton;
        
        private LevelSO _currentFocusLevel;
        
        public static event Action<LevelSO> OnClickLevelStartButton;

        private void Awake()
        {
            if (levelPickUI is null)
            {
                throw new InvalidOperationException(nameof(levelPickUI));
            }
            
            if (levelInformationUI is null)
            {
                throw new InvalidOperationException(nameof(levelInformationUI));
            }
            
            if (levelStartButton is null)
            {
                throw new InvalidOperationException(nameof(levelStartButton));
            }
            
            #region 設定初始專注關卡
                _currentFocusLevel = levelPickUI.DefaultLevel;
            #endregion

            #region 初始化關卡選擇介面
                levelPickUI.Initialize();
            #endregion
            
            #region 初始化關卡資訊介面
                levelInformationUI.Initialize(_currentFocusLevel);
            #endregion
            
            #region 按鈕訂閱
                foreach (var levelPickButton in levelPickUI.LevelPickButtons)
                {
                    levelPickButton.OnClick += OnLevelPickButtonClicked;
                }
                
                levelStartButton.OnClick += OnLevelStartButtonClicked;
            #endregion
        }

        private void OnDisable()
        {
            levelStartButton.OnClick -= OnLevelStartButtonClicked;
        }

        private void OnDestroy()
        {
            foreach (var levelPickButton in levelPickUI.LevelPickButtons)
            {
                levelPickButton.OnClick -= OnLevelPickButtonClicked;
            }
            
            levelStartButton.OnClick -= OnLevelStartButtonClicked;
        }
        
        private void OnLevelPickButtonClicked(LevelSO levelData)
        {
            _currentFocusLevel = levelData;
            levelInformationUI.Refresh(_currentFocusLevel);
        }
        
        private void OnLevelStartButtonClicked()
        {
            if (OnClickLevelStartButton is null)
            {
                throw new InvalidOperationException(nameof(OnClickLevelStartButton));
            }
            
            OnClickLevelStartButton.Invoke(_currentFocusLevel);
        }
    }
}