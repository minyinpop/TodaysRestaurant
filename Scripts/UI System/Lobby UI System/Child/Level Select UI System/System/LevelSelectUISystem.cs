using System;
using Common.Object;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.System
{
    public sealed class LevelSelectUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Button levelSelectButton;
                                private Action _levelSelectButtonCleanupAction;

        private void Awake()
        {
            if (levelSelectButton == null)
            {
                Debug.Log($"{nameof(LevelSelectUISystem)} > {nameof(levelSelectButton)} cannot be null.");
            }
            else
            {
                levelSelectButton.OnClicked += TriggerLevelSelectUI;
                _levelSelectButtonCleanupAction = () =>
                {
                    levelSelectButton.OnClicked -= TriggerLevelSelectUI;
                    _levelSelectButtonCleanupAction = null;
                };
            }
        }
        
        private void OnDestroy()
        {
            _levelSelectButtonCleanupAction?.Invoke();
        }

        public void TriggerLevelSelectUI()
        {
        }
    }
}