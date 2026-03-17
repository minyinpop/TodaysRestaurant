using System;
using System.Collections;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common;
using Common.Level.Main;
using Common.Scene_Name;
using DG.Tweening;
using Dialogue_System.Utage;
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
        [field: SerializeField] private SceneNameSO dialogueSceneNameData;
        [field: SerializeField] private SceneNameSO exploreSceneNameData;
        
        private IEnumerator _changeSceneCoroutine;

        private static GameObject _instance;

        private void Awake()
        {
            if (animation is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(animation)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (dialogueSceneNameData is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(dialogueSceneNameData)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (exploreSceneNameData is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(exploreSceneNameData)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            #region Singleton
                if (_instance is not null)
                {
                    Destroy(gameObject);
                    return;
                }

                _instance = gameObject;
                DontDestroyOnLoad(gameObject);
            #endregion
            
            TitleSystem.OnClickStartGameButton += ChangeScene;
            LevelSelectUI.OnClickLevelStartButton += GoToExplore;
            
            UtageReceiveMessageSystem.ChangeScene += ChangeScene;
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
            TitleSystem.OnClickStartGameButton -= ChangeScene;
            LevelSelectUI.OnClickLevelStartButton -= GoToExplore;
            
            UtageReceiveMessageSystem.ChangeScene -= ChangeScene;
        }
        
        private void ChangeScene(string sceneName)
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: sceneName,
                onSceneLoaded: onComplete => onComplete.Invoke());
            StartCoroutine(_changeSceneCoroutine);
        }
        
        private void ChangeScene(string sceneName, Action onComplete)
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: sceneName,
                onSceneLoaded: onComplete => onComplete.Invoke(),
                onComplete: onComplete);
            StartCoroutine(_changeSceneCoroutine);
        }
        
        private void GoToDialogue(string sceneName)
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: sceneName,
                onSceneLoaded: onComplete => onComplete.Invoke());
            StartCoroutine(_changeSceneCoroutine);
        }

        private void GoToExplore(LevelSO levelData)
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: exploreSceneNameData.SceneName,
                onSceneLoaded: onComplete =>
                {
                    var scene = SceneManager.GetSceneByName(exploreSceneNameData.SceneName);
                    var rootObjects = scene.GetRootGameObjects();
                    
                    foreach (var rootObject in rootObjects)
                    {
                        if (rootObject.TryGetComponent<SceneStarter>(out var sceneStarter))
                        {
                            sceneStarter.StartSystem(levelData, onComplete);
                            break;
                        }
                    }
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
                    Debug.Log($"{name} > {GetType().Name} > cannot find the new scene named: {sceneName}.");
                    Destroy(gameObject);
                    yield break;
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