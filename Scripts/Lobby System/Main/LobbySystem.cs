using System;
using Input_System.Main;
using Lobby_System.Child;
using UnityEngine;

namespace Lobby_System.Main
{
    public sealed class LobbySystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private MapSystem mapSystem;
        
        private Action _onPerformedMapCleanupAction;
        
        private void Awake()
        {
            if (mapSystem == null)
            {
                Debug.Log($"{nameof(LobbySystem)} > {nameof(mapSystem)} cannot be null.");
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
            mapSystem.RequireMapUI();
        }
    }
}