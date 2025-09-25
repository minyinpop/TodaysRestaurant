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
        }

        private void OnOptionButtonClicked()
        {
            MessageSystem.ShowOptionUI(
                content: new PopUpUIContent(
                    message: "設定選單",
                    confirmButtonTitle: string.Empty,
                    cancelButtonTitle: string.Empty,
                    closeButtonTitle: string.Empty),
                onClose: () =>
                {
                    Debug.Log("Close Option UI.");
                });
        }
        
        private void OnQuitButtonClicked()
        {
        }
        #endregion
    }
}