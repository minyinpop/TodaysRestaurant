using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.System;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Main
{
    public sealed class LobbyUISystem : MonoBehaviour
    {
        [field: Header("Systems")]
        [field: SerializeField] private LevelSelectUISystem levelSelectUISystem;
                                private static LevelSelectUISystem _levelSelectUISystem;
        
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
        }

        public static void TriggerLevelSelectUI()
        {
        }
    }
}