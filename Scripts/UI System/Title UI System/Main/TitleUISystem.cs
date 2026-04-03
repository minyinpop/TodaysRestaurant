using System;
using Common.Button;
using Common.Database;
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

        /// <summary>
        /// 登入帳號（用於完成新手教學的舊帳號）
        /// </summary>
        public static event Action OnLoginGame;
        /// <summary>
        /// 開始新手教學（用於第一次創建帳號）
        /// </summary>
        /// <param name="label">
        /// 開始的章節名稱
        /// </param>
        /// <param name="page">
        /// 開始於該章節的第幾行
        /// </param>>
        public static event Action<string, int> OnStartTutorial;

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

        private void OnLoginSuccess(bool isNewAccount)
        {
            #region 必要條件檢查
                if (OnLoginGame is null)
                {
                    throw new InvalidOperationException(nameof(OnLoginGame));
                }

                if (OnStartTutorial is null)
                {
                    throw new InvalidOperationException(nameof(OnStartTutorial));
                }
            #endregion
            
            #region 初始化資料庫
                ItemDatabase.Initialize();
                LevelDatabase.Initialize();
                CharacterDatabase.Initialize();
            #endregion

            if (isNewAccount)
            {
                Debug.Log("登入源：已完成新手教學的帳號");
                OnLoginGame.Invoke();
            }
            else
            {
                Debug.Log("登入源：未完成新手教學的帳號");
                OnStartTutorial.Invoke("Start", 0);
            }
        }
    }
}