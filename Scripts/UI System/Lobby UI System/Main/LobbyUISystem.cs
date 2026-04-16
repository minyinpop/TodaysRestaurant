using System;
using Common.Button;
using Common.Database;
using Common.Scene_Name;
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
        [field: SerializeField] private Button restaurantButton;
        
        public static event Action OnClickLevelSelectButtonEvent;
        public static event Action<SceneNameSO> OnClickRestaurantButtonEvent;
        
        private void Awake()
        {
            if (levelSelectUISystem is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(levelSelectUISystem)} cannot be null.");
            }

            if (levelSelectButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(levelSelectButton)} cannot be null.");
            }

            if (restaurantButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(restaurantButton)} cannot be null.");
            }

            _levelSelectUISystem = levelSelectUISystem;
            
            levelSelectButton.OnClick += OnClickLevelSelectButton;
            restaurantButton.OnClick += OnClickRestaurantButton;
        }
        
        private void OnDestroy()
        {
            levelSelectButton.OnClick -= OnClickLevelSelectButton;
            restaurantButton.OnClick -= OnClickRestaurantButton;
        }

        private void OnClickLevelSelectButton()
        {
            if (OnClickLevelSelectButtonEvent is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickLevelSelectButtonEvent)} cannot be null.");
            }

            OnClickLevelSelectButtonEvent.Invoke();
        }
        
        private void OnClickRestaurantButton()
        {
            if (OnClickRestaurantButtonEvent is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickRestaurantButtonEvent)} cannot be null.");
            }

            SceneNameDatabase.GetSceneName(SceneNameType.Restaurant_Scene, out var sceneNameData);
            OnClickRestaurantButtonEvent.Invoke(sceneNameData);
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