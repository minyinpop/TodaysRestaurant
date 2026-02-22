using System;
using Common.Button;
using Common.Level.Main;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Pick_UI.Child
{
    [RequireComponent(typeof(Button))]
    public sealed class LevelPickButton : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Button button;

        private LevelSO _levelData;
        public LevelSO LevelData => _levelData;

        private bool _initialized;

        public event Action<LevelSO> OnClick;

        private void Awake()
        {
            button.OnClick += OnClickEvent;
        }

        private void OnDestroy()
        {
            button.OnClick -= OnClickEvent;
        }

        public void Initialize(LevelSO levelData)
        {
            if (!_initialized)
            {
                _initialized = true;
                
                if (levelData == null)
                {
                    Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(levelData)} cannot be null.");
                }
                else
                {
                    _levelData = levelData;
                    button.SetTitle(_levelData.LevelName);
                }
            }
        }

        private void OnClickEvent()
        {
            if (OnClick == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(OnClick)} cannot be null.");
            }
            else
            {
                OnClick.Invoke(_levelData);
            }
        }
    }
}