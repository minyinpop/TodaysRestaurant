using System;
using Common.Object;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Main;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.System
{
    public sealed class LevelSelectUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private LevelSelectUI levelSelectUI;

        private void Awake()
        {
            if (levelSelectUI == null)
            {
                Debug.Log($"{nameof(LevelSelectUISystem)} > {nameof(levelSelectUI)} cannot be null.");
            }
            else
            {
                levelSelectUI.gameObject.SetActive(false);
            }
        }

        public void ShowLevelSelectUI()
        {
            levelSelectUI.gameObject.SetActive(true);
        }
        
        public void HideLevelSelectUI()
        {
            levelSelectUI.gameObject.SetActive(false);
        }
    }
}