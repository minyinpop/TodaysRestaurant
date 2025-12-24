using System;
using Common.Object;
using Common.Value;
using Message_System.System.Main;
using UnityEngine;

namespace Title_System
{
    internal sealed class TitleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private MessageSystem MessageSystem;
        
        [field: Header("Button")]
        [field: SerializeField] private Button StartButton;
        [field: SerializeField] private Button OptionButton;
        [field: SerializeField] private Button QuitButton;

        public static event Action<string, Action> OnClickStartGameButton;
        public static event Action<string, int> StartScenario;

        private void OnEnable()
        {
            StartButton.onClick += OnStartButtonClicked;
            StartButton.SetInteractable(true);
            OptionButton.onClick += OnOptionButtonClicked;
            OptionButton.SetInteractable(true);
            QuitButton.onClick += OnQuitButtonClicked;
            QuitButton.SetInteractable(true);
        }

        private void OnDisable()
        {
            StartButton.onClick -= OnStartButtonClicked;
            OptionButton.onClick -= OnOptionButtonClicked;
            QuitButton.onClick -= OnQuitButtonClicked;
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
            MessageSystem.ShowOptionUI(
                content: new PopUpUIContent(
                    message: string.Empty,
                    confirmButtonTitle: string.Empty,
                    cancelButtonTitle: string.Empty,
                    closeButtonTitle: string.Empty),
                onClose: () =>
                {
                    // TODO
                });
        }
        
        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
        #endregion
    }
}