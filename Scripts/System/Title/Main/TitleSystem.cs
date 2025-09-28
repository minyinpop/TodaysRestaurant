using System.Message.Main;
using Data.General;
using General.Object;
using UnityEngine;

namespace System.Title.Main
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
        public static event Action<string, int> PlayDialogue;

        private void OnEnable()
        {
            StartButton.OnClick += OnStartButtonClicked;
            OptionButton.OnClick += OnOptionButtonClicked;
            QuitButton.OnClick += OnQuitButtonClicked;
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
            OnClickStartGameButton?.Invoke("Dialogue System",
                () =>
                {
                    // onComplete
                    PlayDialogue?.Invoke("Start", 0);
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