using System.Collections.Generic;
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
        [field: SerializeField] private LevelSO[] levelsData;

        private bool _initialized;

        private void Awake()
        {
            if (buttonContainerParent is null)
            {
                Debug.Log($"{nameof(LevelPickUI)} > {nameof(buttonContainerParent)} cannot be null.");
                return;
            }
            
            if (buttonContainerPrefab is null)
            {
                Debug.Log($"{nameof(LevelPickUI)} > {nameof(buttonContainerPrefab)} cannot be null.");
                return;
            }
            
            if (buttonPrefab is null)
            {
                Debug.Log($"{nameof(LevelPickUI)} > {nameof(buttonPrefab)} cannot be null.");
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
                
                foreach (var levelData in levelsData)
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