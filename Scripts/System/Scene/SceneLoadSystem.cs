using System.Collections;
using System.General;
using System.Title.Main;
using Data.Animation.DOTween.Basic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace System.Scene
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class SceneLoadSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Loading UI")]
        [field: SerializeField] private GameObject UI;
        [field: SerializeField] private CanvasGroup UICanvasGroup;
        [field: SerializeField] private Slider ProgressBar;
        
        [field: Header("Progress Bar Handler")]
        [field: SerializeField] private RectTransform HandlerRect;
        [field: SerializeField] private GameObject LoadingImage;
        [field: SerializeField] private GameObject CompleteImage;
        
        private IEnumerator ChangeSceneCor;

        private static GameObject Instance;

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            TitleSystem.OnClickStartGameButton += ChangeScene;
        }

        private void OnDisable()
        {
            TitleSystem.OnClickStartGameButton -= ChangeScene;
            if (ChangeSceneCor is not null)
            {
                StopCoroutine(ChangeSceneCor);
                ChangeSceneCor = null;
            }
        }

        private void ChangeScene(string sceneName, Action onComplete)
        {
            ChangeSceneCor = ChangeSceneCoroutine();
            StartCoroutine(ChangeSceneCor);
            return;

            IEnumerator ChangeSceneCoroutine()
            {
                var complete = false;
                UI.SetActive(true);
                DoAnimation.DoFade_CanvasGroup(
                    canvasGroup: UICanvasGroup,
                    settings: new DoFade_CanvasGroup(true, 1, Ease.Linear),
                    onComplete: () =>
                    {
                        complete = true;
                    });
                yield return new WaitUntil(() => complete);
                var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                operation.allowSceneActivation = false;
                while (ProgressBar.value < .9f)
                {
                    var progress = Mathf.Clamp01(operation.progress / .9f);
                    ProgressBar.SetValueWithoutNotify(progress);
                    yield return null;
                }
                
                complete = false;
                DoAnimation.DoScale(
                    rect: HandlerRect,
                    settings: new DoScale(Vector2.zero, .5f, Ease.OutBounce),
                    onComplete: () =>
                    {
                        LoadingImage.SetActive(false);
                        CompleteImage.SetActive(true);
                        DoAnimation.DoScale(
                            rect: HandlerRect,
                            settings: new DoScale(Vector2.one, .5f, Ease.OutBounce),
                            onComplete: () =>
                            {
                                complete = true;
                            });
                    });
                yield return new WaitUntil(() => complete);
                operation.allowSceneActivation = true;
                complete = false;
                DoAnimation.DoFade_CanvasGroup(
                    canvasGroup: UICanvasGroup,
                    settings: new DoFade_CanvasGroup(false, 1, Ease.Linear),
                    onComplete: () =>
                    {
                        LoadingImage.SetActive(true);
                        CompleteImage.SetActive(false);
                        UI.SetActive(false);
                        complete = true;
                    });
                yield return new WaitUntil(predicate: () => complete);
                onComplete?.Invoke();
            }
        }
    }
}