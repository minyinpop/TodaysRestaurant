using System;
using System.Collections;
using Common.Object;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Title_System
{
    internal sealed class TitleSystem : MonoBehaviour
    {
        [field: Header("Button")]
        [field: SerializeField] private Button StartButton;
        [field: SerializeField] private Button OptionButton;
        [field: SerializeField] private Button QuitButton;

        public static event Action<string, Action> OnClickStartGameButton;
        public static event Action<string, int> StartScenario;

        private void OnEnable()
        {
            StartButton.OnClicked += OnStartButtonClicked;
            StartButton.SetInteractable(true);
            OptionButton.OnClicked += OnOptionButtonClicked;
            OptionButton.SetInteractable(true);
            QuitButton.OnClicked += OnQuitButtonClicked;
            QuitButton.SetInteractable(true);
        }

        private void OnDisable()
        {
            StartButton.OnClicked -= OnStartButtonClicked;
            OptionButton.OnClicked -= OnOptionButtonClicked;
            QuitButton.OnClicked -= OnQuitButtonClicked;
        }

        #region Button
        private void OnStartButtonClicked()
        {
            OnClickStartGameButton?.Invoke("Dialogue ( Dev )",
                () =>
                {
                    // onComplete
                    StartScenario?.Invoke("Start", 0);
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