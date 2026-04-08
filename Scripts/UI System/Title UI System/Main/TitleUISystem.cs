using System;
using System.IO;
using Common.Button;
using Common.Data_Saver.Player_Inventory_Saver.Main;
using Common.Database;
using Common.Dialogue.Data;
using Common.Dialogue.SO.Main;
using Common.Scene_Name;
using Common.Value;
using UI_System.Message_UI_System.Main;
using UI_System.Title_UI_System.Child.Account_UI_System;
using UnityEngine;

namespace UI_System.Title_UI_System.Main
{
    internal sealed class TitleUISystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private AccountUISystem accountUISystem;
        
        [field: Header("Button")]
        [field: SerializeField] private Button startButton;
        [field: SerializeField] private Button optionButton;
        [field: SerializeField] private Button quitButton;
        [field: SerializeField] private Button dataCleanButton;

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
            if (accountUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(accountUISystem)} 沒有被掛載。");
            }

            if (startButton is null)
            {
                throw new InvalidOperationException($"{nameof(startButton)} 沒有被掛載。");
            }
            
            if (optionButton is null)
            {
                throw new InvalidOperationException($"{nameof(optionButton)} 沒有被掛載。");
            }
            
            if (quitButton is null)
            {
                throw new InvalidOperationException($"{nameof(quitButton)} 沒有被掛載。");
            }

            if (dataCleanButton is null)
            {
                throw new InvalidOperationException($"{nameof(dataCleanButton)} 沒有被掛載。");
            }
            
            startButton.OnClick += OnStartButtonClicked;
            optionButton.OnClick += OnOptionButtonClicked;
            quitButton.OnClick += OnQuitButtonClicked;
            dataCleanButton.OnClick += OnDataCleanButtonClicked;

            accountUISystem.OnLoginSuccess += OnLoginSuccess;
        }

        private void OnDestroy()
        {
            startButton.OnClick -= OnStartButtonClicked;
            optionButton.OnClick -= OnOptionButtonClicked;
            quitButton.OnClick -= OnQuitButtonClicked;
            dataCleanButton.OnClick -= OnDataCleanButtonClicked;
            
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
                        var filePath = Application.persistentDataPath;
                        
                        if (File.Exists(filePath))
                        {
                            Directory.Delete(filePath, true);
                        }
                        
                        Application.Quit();
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
                DialogueDatabase.Initialize();
                SceneNameDatabase.Initialize();
            #endregion

            #region 初始化玩家物品資料
                PlayerInventorySaver.InitializeInventoryToLocal();
            #endregion
            
            if (isNewAccount)
            {
                Debug.Log("登入源：已完成新手教學的帳號"); // TODO Delete
                
                SceneNameDatabase.GetSceneName(SceneNameType.Lobby_Scene, out var sceneNameData);
                
                OnLoginGame.Invoke(sceneNameData);
            }
            else
            {
                Debug.Log("登入源：未完成新手教學的帳號"); // TODO Delete
                
                SceneNameDatabase.GetSceneName(SceneNameType.Dialogue_Scene, out var sceneNameData);
                DialogueDatabase.GetDialogue(DialogueType.Tutorial_01, out var dialogueData);
                
                OnStartTutorial.Invoke(sceneNameData, dialogueData);
            }
        }

    }
}