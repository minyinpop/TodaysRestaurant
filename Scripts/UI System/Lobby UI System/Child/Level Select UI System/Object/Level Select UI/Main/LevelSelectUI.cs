using System;
using System.Collections.Generic;
using Common.Data.Level.Main;
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
        
        [field: Header("Data")]
        [field: SerializeField] private LevelSO defaultLevel;

        private LevelSO _currentFocusLevel;

        private readonly Queue<Action> _levelPickButtonCleanupActions = new();

        private void Awake()
        {
            if (levelPickUI == null)
            {
                Debug.Log($"{nameof(LevelSelectUI)} > {nameof(levelPickUI)} cannot be null.");
                return;
            }
            
            if (levelInformationUI == null)
            {
                Debug.Log($"{nameof(LevelSelectUI)} > {nameof(levelInformationUI)} cannot be null.");
                return;
            }

            if (defaultLevel == null)
            {
                Debug.Log($"{nameof(LevelSelectUI)} > {nameof(defaultLevel)} cannot be null.");
                return;
            }

            _currentFocusLevel = defaultLevel;

            #region Level Pick UI
                levelPickUI.Initialize(out var levelPickButtons);

                foreach (var levelPickButton in levelPickButtons)
                {
                    levelPickButton.OnClick += OnClickLevelPickButton;
                    _levelPickButtonCleanupActions.Enqueue(() => levelPickButton.OnClick -= OnClickLevelPickButton);
                }
            #endregion
            
            #region Level Information UI
                levelInformationUI.Initialize(_currentFocusLevel);
            #endregion

            return;

            void OnClickLevelPickButton(LevelSO levelData)
            {
                Debug.Log($"{levelData.name}");
            }
        }

        private void OnDestroy()
        {
            while (_levelPickButtonCleanupActions.Count > 0)
            {
                _levelPickButtonCleanupActions.Dequeue()?.Invoke();
            }
        }
    }
}