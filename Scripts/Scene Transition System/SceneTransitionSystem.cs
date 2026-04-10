using System;
using System.Collections;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Scene_Name;
using Common.Scene_Starter;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.System.Main;
using Explore_System.System.Main;
using Tutorial_System;
using UI_System.Dialogue_UI_System.Main;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Main;
using UI_System.Lobby_UI_System.Main;
using UI_System.Title_UI_System.Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene_Transition_System
{
    [RequireComponent(typeof(DoAnimation))]
    public class SceneTransitionSystem : MonoBehaviour
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
        
        private IEnumerator _changeSceneCoroutine;

        private static GameObject _instance;

        private void Awake()
        {
            #region 必要條件檢查
                if (animation is null)
                {
                    throw new InvalidOperationException(nameof(animation));
                }

                if (canvas is null)
                {
                    throw new InvalidOperationException(nameof(canvas));
                }

                if (canvasGroup is null)
                {
                    throw new InvalidOperationException(nameof(canvasGroup));
                }

                if (progressBar is null)
                {
                    throw new InvalidOperationException(nameof(progressBar));
                }

                if (handlerRect is null)
                {
                    throw new InvalidOperationException(nameof(handlerRect));
                }

                if (loadingImage is null)
                {
                    throw new InvalidOperationException(nameof(loadingImage));
                }

                if (completeImage is null)
                {
                    throw new InvalidOperationException(nameof(completeImage));
                }
            #endregion

            if (_instance is not null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = gameObject;
            DontDestroyOnLoad(gameObject);
            
            #region 標題場景訂閱
                TitleUISystem.OnLoginGame += ChangeScene;
                TitleUISystem.OnStartTutorial += ChangeScene;
            #endregion
            
            #region 大廳場景訂閱
                LobbyUISystem.OnClickRestaurantButtonEvent += ChangeScene;
                LevelSelectUI.OnClickLevelStartButton += ChangeScene;
            #endregion
            
            #region 對話場景訂閱
                DialogueUISystem.OnChangeScene += ChangeScene;
            #endregion
            
            #region 戰鬥場景訂閱
                BattleSystem.OnClickEnemyWinConfirmButton += ChangeScene;
            #endregion
            
            #region 探索場景訂閱
                ExploreSystem.OnLevelExplore += ChangeScene;
            #endregion
            
            #region 比賽投稿用專用訂閱
                MissionUI_DevelopOnly.ChangeScene_DevelopOnly += ChangeScene;
                RestaurantTutorialSystem.OnTutorialComplete += ChangeScene;
                BattleTutorialSystem.OnTutorialComplete += ChangeScene;
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
            #region 標題場景訂閱
                TitleUISystem.OnLoginGame -= ChangeScene;
                TitleUISystem.OnStartTutorial -= ChangeScene;
            #endregion
            
            #region 大廳場景訂閱
                LobbyUISystem.OnClickRestaurantButtonEvent -= ChangeScene;
                LevelSelectUI.OnClickLevelStartButton -= ChangeScene;
            #endregion
            
            #region 對話場景訂閱
                DialogueUISystem.OnChangeScene -= ChangeScene;
            #endregion
            
            #region 戰鬥場景訂閱
                BattleSystem.OnClickEnemyWinConfirmButton -= ChangeScene;
            #endregion
            
            #region 探索場景訂閱
                ExploreSystem.OnLevelExplore -= ChangeScene;
            #endregion
            
            #region 比賽投稿用專用訂閱
                MissionUI_DevelopOnly.ChangeScene_DevelopOnly -= ChangeScene;
                RestaurantTutorialSystem.OnTutorialComplete -= ChangeScene;
                BattleTutorialSystem.OnTutorialComplete -= ChangeScene;
            #endregion
        }

        private void ChangeScene(SceneNameSO sceneNameData)
        {
            SceneStarter sceneStarter = null;
            var systemFound = false;

            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: sceneNameData.SceneName,
                onSceneLoaded: onComplete =>
                {
                    var scene = SceneManager.GetSceneByName(sceneNameData.SceneName);
                    var rootObjects = scene.GetRootGameObjects();

                    foreach (var rootObject in rootObjects)
                    {
                        if (rootObject.TryGetComponent(out sceneStarter))
                        {
                            systemFound = true;

                            sceneStarter.StartSystem(onComplete);
                            break;
                        }
                    }
                    
                    if (!systemFound)
                    {
                        throw new InvalidOperationException(nameof(SceneStarter));
                    }
                },
                onComplete: () =>
                {
                    sceneStarter.StartSystemWhenFinish();
                });
            StartCoroutine(_changeSceneCoroutine);
        }

        private void ChangeScene(SceneNameSO sceneNameData, SceneStarterData starterData)
        {
            SceneStarter sceneStarter = null;
            var systemFound = false;

            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: sceneNameData.SceneName,
                onSceneLoaded: onComplete =>
                {
                    var scene = SceneManager.GetSceneByName(sceneNameData.SceneName);
                    var rootObjects = scene.GetRootGameObjects();

                    foreach (var rootObject in rootObjects)
                    {
                        if (rootObject.TryGetComponent(out sceneStarter))
                        {
                            systemFound = true;

                            sceneStarter.StartSystem(starterData, onComplete);
                            break;
                        }
                    }
                    
                    if (!systemFound)
                    {
                        throw new InvalidOperationException(nameof(SceneStarter));
                    }
                },
                onComplete: () =>
                {
                    sceneStarter.StartSystemWhenFinish(starterData);
                });
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