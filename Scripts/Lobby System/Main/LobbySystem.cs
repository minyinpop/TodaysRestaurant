using System;
using Common.Scene_Starter;
using Lobby_System.Child;
using UI_System.Lobby_UI_System.Main;
using UnityEngine;

namespace Lobby_System.Main
{
    public sealed class LobbySystem : SceneStarter
    {
        [field: Header("Systems")]
        [field: SerializeField] private LevelSelectSystem levelSelectSystem;
        
        private bool _isLevelSelectUIOpen;

        private Action _onClickLevelSelectButtonCleanupAction;
        
        private void Awake()
        {
            if (levelSelectSystem is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(levelSelectSystem)} cannot be null.");
            }
            
            LobbyUISystem.OnClickLevelSelectButtonEvent += OnPerformLevelSelectButton;
            _onClickLevelSelectButtonCleanupAction = () =>
            {
                LobbyUISystem.OnClickLevelSelectButtonEvent -= OnPerformLevelSelectButton;
                _onClickLevelSelectButtonCleanupAction = null;
            };
        }
        
        private void OnDestroy()
        {
            _onClickLevelSelectButtonCleanupAction?.Invoke();
        }

        public override void StartSystem(Action onComplete)
        {
            onComplete.Invoke();
        }

        private void OnPerformLevelSelectButton()
        {
            _isLevelSelectUIOpen = !_isLevelSelectUIOpen;
            
            if (_isLevelSelectUIOpen)
            {
                levelSelectSystem.HideLevelSelectUI();
            }
            else
            {
                levelSelectSystem.ShowLevelSelectUI();
            }
        }
    }
}