using System;
using Common.Object;
using UI_System.Lobby_UI_System.Main;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child
{
    public sealed class FastButtonUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Button mapFastUIButton;
                                private Action _mapFastUIButtonCleanupAction;

        private void Awake()
        {
            if (mapFastUIButton == null)
            {
                Debug.Log($"{nameof(FastButtonUISystem)} > {nameof(mapFastUIButton)} cannot be null.");
            }
            else
            {
                mapFastUIButton.OnClicked += LobbyUISystem.RequireMapUI;
                _mapFastUIButtonCleanupAction = () =>
                {
                    mapFastUIButton.OnClicked -= LobbyUISystem.RequireMapUI;
                    _mapFastUIButtonCleanupAction = null;
                };
            }
        }
        
        private void OnDestroy()
        {
            _mapFastUIButtonCleanupAction?.Invoke();
        }
    }
}