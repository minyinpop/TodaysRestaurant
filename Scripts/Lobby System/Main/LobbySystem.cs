using System;
using Input_System;
using Input_System.Main;
using Lobby_System.Child;
using UI_System.Lobby_UI_System.Main;
using UnityEngine;

namespace Lobby_System.Main
{
    public sealed class LobbySystem : MonoBehaviour
    {
        [field: Header("Systems")]
        [field: SerializeField] private LevelSelectSystem levelSelectSystem;
        
        private bool _isLevelSelectUIOpen;

        private Action _onLobbyLevelSelectUIPerformedCleanupAction;
        private Action _onClickLevelSelectButtonCleanupAction;
        
        private void Awake()
        {
            if (levelSelectSystem == null)
            {
                Debug.Log($"{nameof(LobbySystem)} > {nameof(levelSelectSystem)} cannot be null.");
                return;
            }
            
            LobbyInputSystem.OnLobbyLevelSelectUIPerformedAction += OnPerformedMap;
            _onLobbyLevelSelectUIPerformedCleanupAction = () =>
            {
                LobbyInputSystem.OnLobbyLevelSelectUIPerformedAction -= OnPerformedMap;
                _onLobbyLevelSelectUIPerformedCleanupAction = null;
            };

            LobbyUISystem.OnClickLevelSelectButtonEvent += OnPerformedMap;
            _onClickLevelSelectButtonCleanupAction = () =>
            {
                LobbyUISystem.OnClickLevelSelectButtonEvent -= OnPerformedMap;
                _onClickLevelSelectButtonCleanupAction = null;
            };
        }
        
        private void OnDestroy()
        {
            _onLobbyLevelSelectUIPerformedCleanupAction?.Invoke();
            _onClickLevelSelectButtonCleanupAction?.Invoke();
        }

        private void OnPerformedMap()
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