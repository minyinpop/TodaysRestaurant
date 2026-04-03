using System;
using System.Collections.Generic;
using Common.Data_Saver.Player_Level_Saver.Main;
using Common.Database;
using Common.Level.Main;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Pick_UI.Child;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Pick_UI.Main
{
    public sealed class LevelPickUI : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private RectTransform buttonContainerParent;
        [field: SerializeField] private GameObject buttonContainerPrefab;
        [field: SerializeField] private LevelPickButton buttonPrefab;
        
        [field: Header("Data")]
        [field: SerializeField] private LevelSO defaultLevel;
                                public LevelSO DefaultLevel => defaultLevel;
                                
        private readonly Queue<LevelPickButton> _levelPickButtons = new();
        public Queue<LevelPickButton> LevelPickButtons => _levelPickButtons;

        private bool _initialized;

        private void Awake()
        {
            if (buttonContainerParent is null)
            {
                throw new InvalidOperationException(nameof(buttonContainerParent));
            }
            
            if (buttonContainerPrefab is null)
            {
                throw new InvalidOperationException(nameof(buttonContainerPrefab));
            }
            
            if (buttonPrefab is null)
            {
                throw new InvalidOperationException(nameof(buttonPrefab));
            }
            
            if (defaultLevel is null)
            {
                throw new InvalidOperationException(nameof(defaultLevel));
            }
        }

        public void Initialize()
        {
            #region 必要條件檢查
                if (_initialized)
                {
                    throw new InvalidOperationException(nameof(_initialized));
                }
            #endregion
            
            _initialized = true;

            if (PlayerLevelSaver.LoadUnlockLevelFromLocal(out var saveData))
            {
                Debug.Log("從本地獲取玩家解鎖的關卡。");

                foreach (var unlockLevel in saveData.UnlockLevels)
                {
                    #region 條件檢查
                        if (!unlockLevel.IsUnlock)
                        {
                            continue;
                        }
                    #endregion
                    
                    #region 生成容器
                        var newContainer = Instantiate(buttonContainerPrefab, buttonContainerParent);
                    #endregion

                    #region 生成關卡選擇按鈕
                        var newButton = Instantiate(buttonPrefab.gameObject, newContainer.transform);
                        var newButton_LevelPickButton = newButton.GetComponent<LevelPickButton>();
                    #endregion

                    #region 從關卡資料庫獲取關卡
                        LevelDatabase.GetLevel(unlockLevel.LevelName, out var levelData);
                    #endregion
                    
                    #region 初始化關卡選擇按鈕
                        newButton_LevelPickButton.Initialize(levelData);
                        _levelPickButtons.Enqueue(newButton_LevelPickButton);
                    #endregion
                }
            }
            else
            {
                Debug.Log("無法從本地獲取玩家解鎖的關卡，使用預設關卡。");
                
                #region 生成容器
                    var newContainer = Instantiate(buttonContainerPrefab, buttonContainerParent);
                #endregion

                #region 生成關卡選擇按鈕
                    var newButton = Instantiate(buttonPrefab.gameObject, newContainer.transform);
                    var newButton_LevelPickButton = newButton.GetComponent<LevelPickButton>();
                #endregion
                    
                #region 初始化關卡選擇按鈕
                    newButton_LevelPickButton.Initialize(defaultLevel);
                    _levelPickButtons.Enqueue(newButton_LevelPickButton);
                #endregion
            }
        }
    }
}