using System;
using Input_System.Main;
using Lobby_System.Child;
using UnityEngine;

namespace Lobby_System.Main
{
    public sealed class LobbySystem : MonoBehaviour
    {
        [field: Header("Systems")]
        [field: SerializeField] private LevelSelectSystem levelSelectSystem;
        
        private Action _onPerformedMapCleanupAction;
        
        private void Awake()
        {
            if (levelSelectSystem == null)
            {
                Debug.Log($"{nameof(LobbySystem)} > {nameof(levelSelectSystem)} cannot be null.");
                return;
            }
            
            InputSystem.OnPerformedMap += OnPerformedMap;
            _onPerformedMapCleanupAction = () =>
            {
                InputSystem.OnPerformedMap -= OnPerformedMap;
                _onPerformedMapCleanupAction = null;
            };
        }
        
        private void OnDestroy()
        {
            _onPerformedMapCleanupAction?.Invoke();
        }

        private void OnPerformedMap()
        {
            levelSelectSystem.TriggerLevelSelectUI();
        }
    }
}