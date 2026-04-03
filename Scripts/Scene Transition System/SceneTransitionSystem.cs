using System;
using System.Collections;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Scene_Name;
using DG.Tweening;
using Dialogue_System.Utage;
using Explore_System.System.Child.Battle_System.System.Main;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Main;
using UI_System.Lobby_UI_System.Main;
using UI_System.Title_UI_System.Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene_Transition_System
{
    [RequireComponent(typeof(DoAnimation))]
    public partial class SceneTransitionSystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Loading UI")]
        [field: SerializeField] private Canvas canvas;
        [field: SerializeField] private CanvasGroup canvasGroup;
        [field: SerializeField] private Slider progressBar;
        
        [field: Header("Progress Bar Handler")]
        [field: SerializeField] private RectTransform handlerRect;
        [field: SerializeField] private Image loadingImage;
        [field: SerializeField] private Image completeImage;
        
        [field: Header("Scene Name")]
        [field: SerializeField] private SceneNameSO dialogueSceneNameData;
        [field: SerializeField] private SceneNameSO lobbySceneNameData;
        [field: SerializeField] private SceneNameSO exploreSceneNameData;
        [field: SerializeField] private SceneNameSO restaurantSceneNameData;
        
        private IEnumerator _changeSceneCoroutine;

        private static GameObject _instance;

        private void Awake()
        {
            #region 必要條件檢查
                if (animation is null)
                {
                    throw new InvalidOperationException(nameof(animation));
                }

                if (dialogueSceneNameData is null)
                {
                    throw new InvalidOperationException(nameof(dialogueSceneNameData));
                }

                if (lobbySceneNameData is null)
                {
                    throw new InvalidOperationException(nameof(lobbySceneNameData));
                }

                if (exploreSceneNameData is null)
                {
                    throw new InvalidOperationException(nameof(exploreSceneNameData));
                }
            #endregion

            if (_instance is not null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = gameObject;
            DontDestroyOnLoad(gameObject);
            
            #region 訂閱各系統的使用需求
                TitleUISystem.OnLoginGame += GoToLobby;
                TitleUISystem.OnStartTutorial += GoToDialogue;
                
                BattleSystem.OnClickEnemyWinConfirmButton += GoToLobby;
                
                LevelSelectUI.OnClickLevelStartButton += GoToExplore;
                
                LobbyUISystem.OnClickRestaurantButtonEvent += GoToRestaurant;
                
                UtageReceiveMessageSystem.GoToExplore += GoToExplore;
            #endregion
        }

        private void OnDisable()
        {
            if (_changeSceneCoroutine is not null)
            {
                StopCoroutine(_changeSceneCoroutine);
                _changeSceneCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            TitleUISystem.OnLoginGame -= GoToLobby;
            TitleUISystem.OnStartTutorial -= GoToDialogue;
            
            BattleSystem.OnClickEnemyWinConfirmButton -= GoToLobby;
            
            LevelSelectUI.OnClickLevelStartButton -= GoToExplore;
            
            LobbyUISystem.OnClickRestaurantButtonEvent -= GoToRestaurant;
            
            UtageReceiveMessageSystem.GoToExplore -= GoToExplore;
        }
        
        private void ChangeScene(string sceneName)
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: sceneName,
                onSceneLoaded: onComplete => onComplete.Invoke());
            StartCoroutine(_changeSceneCoroutine);
        }

        private IEnumerator ChangeSceneCoroutine(string sceneName, Action<Action> onSceneLoaded, Action onComplete = null)
        {
            #region 淡入過場
                var complete = false;
                canvas.gameObject.SetActive(true);
                animation.DoFade_CanvasGroup(
                    canvasGroup: canvasGroup,
                    settings: new DoFade_CanvasGroup(1, 1, Ease.Linear),
                    onComplete: () =>
                    {
                        complete = true;
                    });
                yield return new WaitUntil(() => complete);
            #endregion
            
            #region 新場景載入
                var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                if (operation is null)
                {
                    throw new InvalidOperationException(nameof(sceneName));
                }
                
                while (operation.progress < .9f)
                {
                    var progress = Mathf.Clamp01(operation.progress / .9f);
                    progressBar.SetValueWithoutNotify(progress);
                    yield return null;
                }
                
                progressBar.SetValueWithoutNotify(1);
                yield return operation;
            #endregion
            
            #region 啟用新場景啟動器
                complete = false;
                onSceneLoaded?.Invoke(() =>
                {
                    complete = true;
                });
                yield return new WaitUntil(() => complete);
            #endregion
            
            #region 載入完成後的蘑菇小圖標動畫
                complete = false;
                animation.DoScale_UI(
                    rect: handlerRect,
                    settings: new DoScale(Vector2.zero, .5f, Ease.OutBounce),
                    onComplete: () =>
                    {
                        loadingImage.gameObject.SetActive(false);
                        completeImage.gameObject.SetActive(true);
                        animation.DoScale_UI(
                            rect: handlerRect,
                            settings: new DoScale(Vector2.one, .5f, Ease.OutBounce),
                            onComplete: () =>
                            {
                                complete = true;
                            });
                    });
                yield return new WaitUntil(() => complete);
            #endregion
            
            #region 淡出過場
                complete = false;
                animation.DoFade_CanvasGroup(
                    canvasGroup: canvasGroup,
                    settings: new DoFade_CanvasGroup(0, 1, Ease.Linear),
                    onComplete: () =>
                    {
                        loadingImage.gameObject.SetActive(true);
                        completeImage.gameObject.SetActive(false);
                        canvas.gameObject.SetActive(false);
                        complete = true;
                    });
                yield return new WaitUntil(() => complete);
                
                progressBar.SetValueWithoutNotify(0);
                onComplete?.Invoke();
            #endregion
        }
    }
}