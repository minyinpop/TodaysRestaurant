using System;
using System.Collections.Generic;
using Common.Level.Main;
using Common.Player.Child.Player_Level;
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
        [field: SerializeField] private PlayerLevelSO playerLevelData;

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
            
            if (playerLevelData is null)
            {
                throw new InvalidOperationException(nameof(playerLevelData));
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
                
                foreach (var levelData in playerLevelData.UnlockLevels)
                {
                    #region Container
                        var newContainer = Instantiate(buttonContainerPrefab, buttonContainerParent);
                    #endregion
                    
                    #region Button
                        var newButton = Instantiate(buttonPrefab.gameObject, newContainer.transform);
                        var newButton_LevelPickButton = newButton.GetComponent<LevelPickButton>();
                        
                        newButton_LevelPickButton.Initialize(levelData);
                        levelPickButtons.Enqueue(newButton_LevelPickButton);
                    #endregion
                }
            }
        }
    }
}