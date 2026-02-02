using UI_System.Lobby_UI_System.Child.Map_UI_System.System;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Main
{
    public sealed class LobbyUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private MapUISystem mapUISystem;
                                private static MapUISystem _mapUISystem;

        private void Awake()
        {
            if (mapUISystem == null)
            {
                Debug.Log($"{nameof(LobbyUISystem)} > {nameof(mapUISystem)} cannot be null.");
            }
            else
            {
                _mapUISystem = mapUISystem;
            }
        }

        public static void RequireMapUI()
        {
            _mapUISystem.RequireUI();
        }
    }
}