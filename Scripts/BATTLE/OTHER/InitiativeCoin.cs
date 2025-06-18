using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.OTHER
{
    internal class InitiativeCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        // Components
        private RectTransform RectTransform { get; set; }
        
        // State
        private bool CanClick { get; set; }

        public static event System.Action FinishFlipEvent;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public void SlideInScreen(RectTransform parent)
        {
            RectTransform.SetParent(parent);

            RectTransform
                .DOAnchorPos(Vector2.zero, .75f, true)
                .SetEase(Ease.OutBack)
                .OnComplete(() => { CanClick = true; });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        public void SlideToMiddle(RectTransform parent)
        {
            // TODO 解決更改父物件會導致位移的問題
            var lastRect = RectTransform.anchoredPosition;
            
            RectTransform.SetParent(parent);
            
            RectTransform.anchoredPosition = lastRect;
            
            DOTween.Sequence()
                .Append(RectTransform
                    .DOAnchorPos(Vector2.zero, .3f, true)
                    .SetEase(Ease.OutQuad))
                .Append(RectTransform
                    .DOScale(Vector2.one * 2, .3f)
                    .SetEase(Ease.InOutBack));
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
            
            DOTween.Sequence()
                .Append(RectTransform
                    .DOScale(Vector2.one, 1)
                    .SetEase(Ease.InBack))
                .JoinCallback(() =>
                {
                    var randomDistanceX = Screen.width * Random.Range(-.25f, .25f);
                    var randomDistanceY = Screen.height * Random.Range(.1f, .5f);
                    
                    RectTransform
                        .DOAnchorPos(new Vector2(randomDistanceX, randomDistanceY), 2, true)
                        .SetEase(Ease.OutQuad);
                })
                .JoinCallback(() =>
                {
                    const int rotateAngleY = 180;
                    var rotateTimesY = Random.Range(8, 16);
                    var randomRotateY = rotateAngleY * rotateTimesY;

                    var rotateAngleZ = Random.Range(0, 361);
                    var rotateTimesZ = Random.Range(8, 16);
                    var randomRotateZ = rotateAngleZ * rotateTimesZ;
                    
                    RectTransform
                        .DORotate(new Vector3(0, randomRotateY, randomRotateZ), 2, RotateMode.FastBeyond360)
                        .SetEase(Ease.OutCubic);
                })
                .AppendInterval(1)
                .OnComplete(() => { FinishFlipEvent?.Invoke(); });
        }
        #endregion
    }
}