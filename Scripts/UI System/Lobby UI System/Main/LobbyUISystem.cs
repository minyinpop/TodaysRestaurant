using System;
using Common.Object;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.System;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Main
{
    public sealed class LobbyUISystem : MonoBehaviour
    {
        [field: Header("Systems")]
        [field: SerializeField] private LevelSelectUISystem levelSelectUISystem;
                                private static LevelSelectUISystem _levelSelectUISystem;
                                
        [field: Header("Objects")]
        [field: SerializeField] private Button levelSelectButton;
        
        private Action _levelSelectButtonCleanupAction;
        public static event Action OnClickLevelSelectButtonEvent;
        
        private void Awake()
        {
            if (levelSelectUISystem == null)
            {
                Debug.Log($"{nameof(LobbyUISystem)} > {nameof(levelSelectUISystem)} cannot be null.");
            }
            else
            {
                _levelSelectUISystem = levelSelectUISystem;
            }
            
            if (levelSelectButton == null)
            {
                Debug.Log($"{nameof(LevelSelectUISystem)} > {nameof(levelSelectButton)} cannot be null.");
            }
            else
            {
                levelSelectButton.OnClicked += OnClickLevelSelectButton;
                _levelSelectButtonCleanupAction = () =>
                {
                    levelSelectButton.OnClicked -= OnClickLevelSelectButton;
                    _levelSelectButtonCleanupAction = null;
                };
            }
        }
        
        private void OnDestroy()
        {
            _levelSelectButtonCleanupAction?.Invoke();
        }

        private void OnClickLevelSelectButton()
        {
            OnClickLevelSelectButtonEvent?.Invoke();
        }

        public static void ShowLevelSelectUI()
        {
            _levelSelectUISystem.ShowLevelSelectUI();
        }
        
        public static void HideLevelSelectUI()
        {
            _levelSelectUISystem.HideLevelSelectUI();
        }
    }
}