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
        }

        public void Initialize(out Queue<LevelPickButton> levelPickButtons)
        {
            if (_initialized)
            {
                Debug.Log($"{nameof(LevelPickUI)} > {nameof(Initialize)} is already initialized.");
                levelPickButtons = null;
            }
            else
            {
                _initialized = true;
                
                levelPickButtons = new Queue<LevelPickButton>();
                
                var data = PlayerUnlockLevelSaver.LoadUnlockLevelFromLocal();
                
                foreach (var unlockLevel in data.UnlockLevels)
                {
                    #region Container
                        var newContainer = Instantiate(buttonContainerPrefab, buttonContainerParent);
                    #endregion
                    
                    #region Button
                        var newButton = Instantiate(buttonPrefab.gameObject, newContainer.transform);
                        var newButton_LevelPickButton = newButton.GetComponent<LevelPickButton>();
                        
                        newButton_LevelPickButton.Initialize(LevelDatabase.GetLevel(unlockLevel.LevelName));
                        levelPickButtons.Enqueue(newButton_LevelPickButton);
                    #endregion
                }
            }
        }
    }
}