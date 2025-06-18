using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.OTHER
{
    internal class InitiativeCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private GameObject HeadsObj { get; set; }
        [field: SerializeField] private GameObject TailsObj { get; set; }
        
        // Components
        private RectTransform ReadyParent { get; set; }
        private RectTransform ShowParent { get; set; }
        private RectTransform RectTransform { get; set; }
        
        // State
        private bool CanClick { get; set; }

        // 當完成
        public static event System.Action<bool> FinishFlipEvent;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void Init(RectTransform ready, RectTransform show)
        {
            ReadyParent = ready;
            ShowParent = show;
            
            DOTween.Sequence()
                .AppendCallback(() => { RectTransform.SetParent(ReadyParent); })
                .Append(RectTransform
                    .DOAnchorPos(Vector2.zero, .75f, true)
                    .SetEase(Ease.OutBack))
                .OnComplete(() => { CanClick = true; });
        }
        
        #region Pointer Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanClick) return;
            RectTransform
                .DOScale(Vector2.one * 2, .25f)
                .SetEase(Ease.OutQuart);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanClick) return;
            RectTransform
                .DOScale(Vector2.one, .25f)
                .SetEase(Ease.OutQuart);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanClick) return;
            CanClick = false;
            
            var randomDistanceX = Screen.width * Random.Range(-.25f, .25f);
            var randomDistanceY = Screen.height * Random.Range(.1f, .5f);
            
            const int rotateAngleY = 180;
            var rotateTimesY = Random.Range(8, 16);
            var randomRotateY = rotateAngleY * rotateTimesY;

            var rotateAngleZ = Random.Range(0, 361);
            var rotateTimesZ = Random.Range(8, 16);
            var randomRotateZ = rotateAngleZ * rotateTimesZ;
            
            DOTween.Sequence()
                .Append(RectTransform
                    .DOScale(Vector2.one, 1)
                    .SetEase(Ease.InBack))
                .JoinCallback(() =>
                {
                    RectTransform
                        .DOAnchorPos(new Vector2(randomDistanceX, randomDistanceY), 2, true)
                        .SetEase(Ease.OutQuad);
                })
                .JoinCallback(() =>
                {
                    RectTransform
                        .DORotate(new Vector3(0, randomRotateY, randomRotateZ), 2, RotateMode.FastBeyond360)
                        .SetEase(Ease.OutCubic)
                        .OnUpdate(() =>
                        {
                            switch (RectTransform.localEulerAngles.y)
                            {
                                case < 270 and > 90 when HeadsObj.activeSelf:
                                    HeadsObj.SetActive(false);
                                    TailsObj.SetActive(true);
                                    break;
                                case >= 270 or <= 90 when TailsObj.activeSelf:
                                    HeadsObj.SetActive(true);
                                    TailsObj.SetActive(false);
                                    break;
                            }
                        });
                })
                .AppendInterval(1)
                .OnComplete(() =>
                {
                    DOTween.Sequence()
                        .AppendCallback(() => { RectTransform.SetParent(ShowParent); })
                        .Append(RectTransform
                            .DOAnchorPos(Vector2.zero, .3f, true)
                            .SetEase(Ease.OutQuad))
                        .Append(RectTransform
                            .DOScale(Vector2.one * 2, .3f)
                            .SetEase(Ease.InOutBack))
                        .AppendInterval(1)
                        .AppendCallback(() =>
                        {
                            FinishFlipEvent?.Invoke(rotateTimesY % 2 == 0);
                        });
                });
        }
        #endregion
    }
}