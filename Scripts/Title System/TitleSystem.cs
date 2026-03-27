using System;
using Common.Button;
using UnityEngine;

namespace Title_System
{
    internal sealed class TitleSystem : MonoBehaviour
    {
        [field: Header("Button")]
        [field: SerializeField] private Button StartButton;
        [field: SerializeField] private Button OptionButton;
        [field: SerializeField] private Button QuitButton;

        public static event Action<Action> OnClickStartGameButton;
        public static event Action<string, int> StartScenario;

        private void Awake()
        {
            StartButton.OnClick += OnStartButtonClicked;
            OptionButton.OnClick += OnOptionButtonClicked;
            QuitButton.OnClick += OnQuitButtonClicked;
        }

        private void OnDestroy()
        {
            StartButton.OnClick -= OnStartButtonClicked;
            OptionButton.OnClick -= OnOptionButtonClicked;
            QuitButton.OnClick -= OnQuitButtonClicked;
        }

        #region Button
            private void OnStartButtonClicked()
            {
                if (OnClickStartGameButton is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(OnClickStartGameButton)} cannot be null.)");
                    Destroy(gameObject);
                    return;
                }
                
                OnClickStartGameButton?.Invoke(() =>
                {
                });
            }

            private void OnOptionButtonClicked()
            {
                // TODO
                Debug.Log("TODO Option UI System");
                // UISystem.ShowOptionUI();
            }
            
            private void OnQuitButtonClicked()
            {
                Application.Quit();
            }
        #endregion
    }
}