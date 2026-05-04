using System;
using System.IO;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Button;
using Common.Database;
using Common.Dialogue.SO.Main;
using Common.Scene_Name;
using Common.Value;
using Spine.Unity;
using UI_System.Message_UI_System.Main;
using UI_System.Title_UI_System.Object;
using UI_System.Title_UI_System.System.Child.Account_UI_System;
using UnityEngine;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace UI_System.Title_UI_System.System.Main
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class TitleUISystem : MonoBehaviour
    {
        [field: Header("系統")]
        [field: SerializeField] private AccountUISystem accountUISystem;
        
        [field: Header("按鈕")]
        [field: SerializeField] private GameObject buttonContainer;
        [field: SerializeField] private Button startButton;
        [field: SerializeField] private Button settingsButton;
        [field: SerializeField] private Button quitButton;
        [field: SerializeField] private Button dataCleanButton;
        
        [field: Header("DOTween 動畫")]
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private DoFade_CanvasGroup buttonShowSettings;
        
        [field: Header("Spine 動畫")]
        [field: SerializeField] private SkeletonGraphic titleSkeletonGraphic;
        [field: SerializeField] private SpineAnimation startAnimation;
        [field: SerializeField] private SpineAnimation settingsAnimation;
        [field: SerializeField] private SpineAnimation quitAnimation;
        [field: SerializeField] private SpineAnimation backAnimation;
        
        [field: Header("標題遮罩")]
        [field: SerializeField] private TitleMaskBackground titleMaskBackground;
        [field: SerializeField] private TitleMaskLogo  titleMaskLogo;
        
        [field: Header("新帳號的開場對話資料")]
        [field: SerializeField] private DialogueSO tutorialDialogueData;

        /// <summary>
        /// 登入帳號（用於完成新手教學的舊帳號）
        /// </summary>
        public static event Action<SceneNameSO> OnLoginGame;
        /// <summary>
        /// 開始新手教學（用於第一次創建帳號）
        /// </summary>
        /// <param name="label">
        /// 開始的章節名稱
        /// </param>
        public static event Action<SceneNameSO, DialogueSO> OnStartTutorial;

        private void Awake()
        {
            titleMaskLogo.OnComplete += OnTitleMaskLogoComplete;
            titleMaskBackground.OnComplete += OnTitleMaskBackgroundComplete;
            
            startButton.OnCursorEnter += OnCursorEnterStartButton;
            startButton.OnCursorExit += OnCursorExitButton;
            startButton.OnClick += OnStartButtonClicked;
            
            settingsButton.OnCursorEnter += OnCursorEnterSettingsButton;
            settingsButton.OnCursorExit += OnCursorExitButton;
            
            quitButton.OnCursorEnter += OnCursorEnterQuitButton;
            quitButton.OnCursorExit += OnCursorExitButton;
            quitButton.OnClick += OnQuitButtonClicked;
            
            dataCleanButton.OnClick += OnDataCleanButtonClicked;

            accountUISystem.OnLoginSuccess += OnLoginSuccess;
        }

        private void Start()
        {
            titleMaskLogo.Show();
        }

        private void OnDestroy()
        {
            titleMaskLogo.OnComplete -= OnTitleMaskLogoComplete;
            titleMaskBackground.OnComplete -= OnTitleMaskBackgroundComplete;
            
            startButton.OnCursorEnter -= OnCursorEnterStartButton;
            startButton.OnCursorExit -= OnCursorExitButton;
            startButton.OnClick -= OnStartButtonClicked;
            
            settingsButton.OnCursorExit -= OnCursorExitButton;
            settingsButton.OnCursorEnter -= OnCursorEnterSettingsButton;
            
            quitButton.OnCursorEnter -= OnCursorEnterQuitButton;
            quitButton.OnCursorExit -= OnCursorExitButton;
            quitButton.OnClick -= OnQuitButtonClicked;
            
            dataCleanButton.OnClick -= OnDataCleanButtonClicked;
            
            accountUISystem.OnLoginSuccess -= OnLoginSuccess;
        }

        #region Logo 動畫
            private void OnTitleMaskLogoComplete()
            {
                titleMaskBackground.Hide();
            }

            private void OnTitleMaskBackgroundComplete()
            {
                buttonContainer.SetActive(true);
                
                titleSkeletonGraphic.freeze = false;
                
                animation.DoFade_CanvasGroup(
                    canvasGroup: buttonContainer.GetComponent<CanvasGroup>(),
                    settings: buttonShowSettings,
                    onComplete: () =>
                    {
                        startButton.SetInteractable(true);
                        settingsButton.SetInteractable(true);
                        quitButton.SetInteractable(true);
                        dataCleanButton.SetInteractable(true);
                        
                        titleMaskBackground.gameObject.SetActive(false);
                        titleMaskLogo.gameObject.SetActive(false);
                    });
            }
        #endregion

        #region 按鈕
            #region 開始按鈕
                private void OnCursorEnterStartButton()
                {
                    startAnimation.GetValues(
                        out var layer,
                        out var animationName,
                        out var loop);
                    
                    titleSkeletonGraphic.AnimationState.SetAnimation(
                        trackIndex: layer,
                        animationName: animationName,
                        loop: loop);
                }

                private void OnStartButtonClicked()
                {
                    accountUISystem.OpenLoginUI();
                }
            #endregion
            
            #region 設定按鈕
                private void OnCursorEnterSettingsButton()
                {
                    settingsAnimation.GetValues(
                        out var layer,
                        out var animationName,
                        out var loop);

                    titleSkeletonGraphic.AnimationState.SetAnimation(
                        trackIndex: layer,
                        animationName: animationName,
                        loop: loop);
                }
            #endregion

            #region 退出按鈕
                private void OnCursorEnterQuitButton()
                {
                    quitAnimation.GetValues(
                        out var layer,
                        out var animationName,
                        out var loop);
                    
                    titleSkeletonGraphic.AnimationState.SetAnimation(
                        trackIndex: layer,
                        animationName: animationName,
                        loop: loop);
                }
                
                private void OnQuitButtonClicked()
                {
                    Application.Quit();
                }
            #endregion
            
            private void OnCursorExitButton()
            {
                backAnimation.GetValues(
                    out var layer,
                    out var animationName,
                    out var loop);
                    
                titleSkeletonGraphic.AnimationState.SetAnimation(
                    trackIndex: layer,
                    animationName: animationName,
                    loop: loop);
            }
            
            private void OnDataCleanButtonClicked()
            {
                MessageUISystem.ShowSwitchUI(
                    content: new PopUpUIContent(
                        message: "<b><color=red>即將刪除所有的本地資料並且會關閉遊戲！</color></b>\n<b><color=red>請確認好真的要執行此操作嗎！</color></b>",
                        confirmButtonTitle: "<b>確定刪除</b>",
                        cancelButtonTitle: "返回",
                        closeButtonTitle: string.Empty),
                    onConfirm: () =>
                    {
                        if (Directory.Exists($"{Application.persistentDataPath}/SaveData"))
                        {
                            PlayerPrefs.SetInt("DeleteAllLocalData", 1);
                            PlayerPrefs.Save();
                            
                            MessageUISystem.ShowTipUI(
                                content: new PopUpUIContent(
                                    message: "已成功刪除所有本地檔案！\n即將重開遊戲！",
                                    confirmButtonTitle: "確認",
                                    cancelButtonTitle: string.Empty,
                                    closeButtonTitle: string.Empty),
                                onConfirm: () =>
                                {
                                    Application.Quit();
                                });
                        }
                        else
                        {
                            MessageUISystem.ShowTipUI(
                                content: new PopUpUIContent(
                                    message: "無法找尋到本地檔案！",
                                    confirmButtonTitle: "確認",
                                    cancelButtonTitle: string.Empty,
                                    closeButtonTitle: string.Empty));
                        }
                    });
            }
        #endregion

        private void OnLoginSuccess(bool isNewAccount)
        {
            #region 必要條件檢查
                if (OnLoginGame is null)
                {
                    throw new InvalidOperationException($"{nameof(OnLoginGame)} 沒有被訂閱。");
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
            
                SceneNameDatabase.Initialize();
            #endregion
            
            if (isNewAccount)
            {
                Debug.Log("登入源：已完成新手教學的帳號");
                
                SceneNameDatabase.GetSceneName(SceneNameType.Lobby_Scene, out var sceneNameData);
                
                OnLoginGame.Invoke(sceneNameData);
            }
            else
            {
                Debug.Log("登入源：未完成新手教學的帳號");
                
                SceneNameDatabase.GetSceneName(SceneNameType.Dialogue_Scene, out var sceneNameData);
                OnStartTutorial.Invoke(sceneNameData, tutorialDialogueData);
            }
        }
    }
}