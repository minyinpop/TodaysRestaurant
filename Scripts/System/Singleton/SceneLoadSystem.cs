using System.Collections;
using System.General;
using Data.Animation.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Singleton
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class SceneLoadSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Object")]
        [field: SerializeField] private CanvasGroup CanvasGroup;

        private IEnumerator ChangeSceneCor;

        private void OnDisable()
        {
            if (ChangeSceneCor is not null)
            {
                StopCoroutine(ChangeSceneCor);
                ChangeSceneCor = null;
            }
        }
        
        public void ChangeScene(string sceneName)
        {
            ChangeSceneCor = ChangeSceneCoroutine();
            StartCoroutine(ChangeSceneCor);
            return;

            IEnumerator ChangeSceneCoroutine()
            {
                var complete = false;
                DoAnimation.DoFade_CanvasGroup(
                    canvasGroup: CanvasGroup,
                    settings: new DoFade_CanvasGroup(1, 3, Ease.Linear),
                    onComplete: () =>
                    {
                        complete = true;
                    });
                yield return new WaitUntil(() => complete);
            }
        }
    }
}