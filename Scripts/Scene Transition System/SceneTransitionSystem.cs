using System;
using System.Collections;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Level.Main;
using DG.Tweening;
using Dialogue_System.Utage;
using Explore_System.System;
using Title_System;
using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scene_Transition_System
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class SceneTransitionSystem : MonoBehaviour
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
        [field: SerializeField] private string sceneNameForDialogue;
        [field: SerializeField] private string sceneNameForExplore;
        
        private IEnumerator _changeSceneCor;

        private static GameObject _instance;

        private LevelSO _pendingLevelData;

        private void Awake()
        {
            #region Singleton
                if (_instance is not null)
                {
                    Destroy(gameObject);
                    return;
                }

                _instance = gameObject;
                DontDestroyOnLoad(gameObject);
            #endregion
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            TitleSystem.OnClickStartGameButton += ChangeScene;
            LevelSelectUI.OnClickLevelStartButton += GoToExplore;
            
            UtageReceiveMessageSystem.ChangeScene += ChangeScene;
        }

        private void OnDisable()
        {
            if (_changeSceneCor is not null)
            {
                StopCoroutine(_changeSceneCor);
                _changeSceneCor = null;
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
            TitleSystem.OnClickStartGameButton -= ChangeScene;
            LevelSelectUI.OnClickLevelStartButton -= GoToExplore;
            
            UtageReceiveMessageSystem.ChangeScene -= ChangeScene;
        }
        
        private void ChangeScene(string sceneName)
        {
            _changeSceneCor = ChangeSceneCoroutine(sceneName);
            StartCoroutine(_changeSceneCor);
        }
        
        private void ChangeScene(string sceneName, Action onComplete)
        {
            _changeSceneCor = ChangeSceneCoroutine(sceneName, onComplete);
            StartCoroutine(_changeSceneCor);
        }
        
        private void GoToDialogue(string sceneName)
        {
            _changeSceneCor = ChangeSceneCoroutine(sceneName);
            StartCoroutine(_changeSceneCor);
        }

        private void GoToExplore(LevelSO levelData)
        {
            _pendingLevelData = levelData;
            
            _changeSceneCor = ChangeSceneCoroutine(sceneNameForExplore);
            StartCoroutine(_changeSceneCor);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == sceneNameForDialogue)
            {
            }
            else if (scene.name == sceneNameForExplore)
            {
                var exploreSystem = FindFirstObjectByType<ExploreSystem>();
                exploreSystem.StartSystem(_pendingLevelData);
            }
        }

        // private void ReloadScene()
        // {
        //     _changeSceneCor = ChangeSceneCoroutine(SceneManager.GetActiveScene().name);
        //     StartCoroutine(_changeSceneCor);
        // }

        private IEnumerator ChangeSceneCoroutine(string sceneName, Action onComplete = null)
        {
            Debug.Log(sceneName);
            
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
                
                var targetScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                if (targetScene is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(targetScene)} cannot find the new scene.");
                    Destroy(gameObject);
                }
                else
                {
                    targetScene.allowSceneActivation = false;
                    while (progressBar.value < .9f)
                    {
                        var progress = Mathf.Clamp01(targetScene.progress / .9f);
                        progressBar.SetValueWithoutNotify(progress);
                        yield return null;
                    }
                    
                    progressBar.SetValueWithoutNotify(1);
                    
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
                    
                    targetScene.allowSceneActivation = true;
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
                    yield return new WaitUntil(predicate: () => complete);
                    
                    progressBar.SetValueWithoutNotify(0);
                    onComplete?.Invoke();
                }
        }
    }
}