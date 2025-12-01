using System.Message.System.Main;
using Data.General;
using Object;
using UnityEngine;

namespace System.Title
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
            StartButton.OnClick += OnStartButtonClicked;
            StartButton.SetInteractable(true);
            OptionButton.OnClick += OnOptionButtonClicked;
            OptionButton.SetInteractable(true);
            QuitButton.OnClick += OnQuitButtonClicked;
            QuitButton.SetInteractable(true);
        }

        private void OnDisable()
        {
            StartButton.OnClick -= OnStartButtonClicked;
            OptionButton.OnClick -= OnOptionButtonClicked;
            QuitButton.OnClick -= OnQuitButtonClicked;
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