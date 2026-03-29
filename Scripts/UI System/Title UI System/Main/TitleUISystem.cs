using System;
using Common.Button;
using UI_System.Title_UI_System.Child.Account_UI_System;
using UnityEngine;

namespace UI_System.Title_UI_System.Main
{
    internal sealed class TitleUISystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private AccountUISystem accountUISystem;
        
        [field: Header("Button")]
        [field: SerializeField] private Button StartButton;
        [field: SerializeField] private Button OptionButton;
        [field: SerializeField] private Button QuitButton;

        public static event Action<Action> OnClickStartGameButton;
        public static event Action<string, int> StartScenario;

        private void Awake()
        {
            if (accountUISystem is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(accountUISystem)} cannot be null.");
            }

            if (StartButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(StartButton)} cannot be null.");
            }
            
            if (OptionButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OptionButton)} cannot be null.");
            }
            
            if (QuitButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(QuitButton)} cannot be null.");
            }
            
            StartButton.OnClick += OnStartButtonClicked;
            OptionButton.OnClick += OnOptionButtonClicked;
            QuitButton.OnClick += OnQuitButtonClicked;

            accountUISystem.OnLoginSuccess += OnLoginSuccess;
        }

        private void OnDestroy()
        {
            StartButton.OnClick -= OnStartButtonClicked;
            OptionButton.OnClick -= OnOptionButtonClicked;
            QuitButton.OnClick -= OnQuitButtonClicked;
            
            accountUISystem.OnLoginSuccess -= OnLoginSuccess;
        }

        #region Button
            private void OnStartButtonClicked()
            {
                accountUISystem.OpenLoginUI();
            }

            private void OnOptionButtonClicked()
            {
            }
            
            private void OnQuitButtonClicked()
            {
                Application.Quit();
            }
        #endregion

        private void OnLoginSuccess()
        {
            if (OnClickStartGameButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickStartGameButton)} has no subscriber.");
            }
            
            OnClickStartGameButton.Invoke(() =>
            {
                Debug.Log("場景切換完畢。");
                /*
                 * TODO 正式版使用
                 * StartScenario.Invoke("Main", 0);
                 */
            });
        }
    }
}