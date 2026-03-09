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
        
        [field: Header("Data")]
        [field: SerializeField] private LevelSO defaultLevel;
                                private LevelSO _currentFocusLevel;
                                
        private readonly Queue<Action> _levelPickButtonCleanupActions = new();
        private Action _levelStartButtonCleanupAction;
        
        public static event Action<LevelSO> OnClickLevelStartButton;

        private void Awake()
        {
            if (levelPickUI == null)
            {
                Debug.Log($"{nameof(LevelSelectUI)} > {nameof(levelPickUI)} cannot be null.");
            }
            else if (levelInformationUI == null)
            {
                Debug.Log($"{nameof(LevelSelectUI)} > {nameof(levelInformationUI)} cannot be null.");
            }
            else if (levelStartButton == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(levelStartButton)} cannot be null.");
            }
            else if (defaultLevel == null)
            {
                Debug.Log($"{nameof(LevelSelectUI)} > {nameof(defaultLevel)} cannot be null.");
            }
            else
            {
                _currentFocusLevel = defaultLevel;

                #region Level Pick UI
                    levelPickUI.Initialize(out var levelPickButtons);

                    foreach (var levelPickButton in levelPickButtons)
                    {
                        levelPickButton.OnClick += OnLevelPickButtonClicked;
                        _levelPickButtonCleanupActions.Enqueue(() => levelPickButton.OnClick -= OnLevelPickButtonClicked);
                    }
                #endregion
                
                #region Level Information UI
                    levelInformationUI.Initialize(_currentFocusLevel);
                #endregion
                
                #region Level Start Button
                    levelStartButton.OnClick += OnLevelStartButtonClicked;
                    _levelStartButtonCleanupAction = () => levelStartButton.OnClick -= OnLevelStartButtonClicked;
                #endregion
            }

            return;

            void OnLevelPickButtonClicked(LevelSO levelData)
            {
                _currentFocusLevel = levelData;
                levelInformationUI.Refresh(_currentFocusLevel);
            }
            
            void OnLevelStartButtonClicked()
            {
                if (OnClickLevelStartButton == null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(OnClickLevelStartButton)} cannot be null.");
                    Destroy(gameObject);
                    return;
                }
                
                OnClickLevelStartButton.Invoke(_currentFocusLevel);
            }
        }

        private void OnDestroy()
        {
            while (_levelPickButtonCleanupActions.Count > 0)
            {
                _levelPickButtonCleanupActions.Dequeue()?.Invoke();
            }
            
            _levelStartButtonCleanupAction?.Invoke();
        }
    }
}