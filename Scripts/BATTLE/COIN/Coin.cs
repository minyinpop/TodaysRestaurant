using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.COIN
{
    internal class Coin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: Header("Coin Transform")]
        [field: SerializeField] private RectTransform CoinRect { get; set; }
        private RectTransform ShowParent { get; set; }
        
        [field: Header("Coin Image")]
        [field: SerializeField] private GameObject FrontImage { get; set; }
        [field: SerializeField] private GameObject BackImage { get; set; }
        
        private bool CanClicked { get; set; } = true;
        
        private Sequence ThrowSequence { get; set; }
        private Sequence ShowSequence { get; set; }

        // Send a boolean value to indicate whether the player is the winner
        public event System.Action<bool> OnShowCompleteEvent;

        public void Init(RectTransform showParent)
        {
            ShowParent = showParent;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanClicked) return;
            CoinRect.DOScale(Vector3.one * 1.5f, .2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanClicked) return;
            CoinRect.DOScale(Vector3.one, .2f);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanClicked) return;
            CanClicked = false;

            var randomXDistance = Random.Range(0, 801) * (Random.Range(-1, 2) == 0 ? -1 : 1);
            var randomYDistance = Random.Range(300, 801);

            var rotXTimes = Random.Range(10, 16);
            var rotZTimes = Random.Range(5, 8);
            var randomXAngle =  rotXTimes * 180;
            var randomZAngle =  rotZTimes * Random.Range(1, 361);
            
            ThrowSequence = DOTween.Sequence();
            ThrowSequence
                .Append(CoinRect.DOScale(Vector3.one, 1)
                    .SetEase(Ease.InBack))
                .Join(CoinRect.DOAnchorPos(new Vector2(randomXDistance, randomYDistance), 2, true)
                    .SetEase(Ease.OutQuad))
                .Join(CoinRect.DORotate(new Vector3(randomXAngle, 0, randomZAngle), 2, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad))
                .OnUpdate(() =>
                {
                    if (Mathf.Abs(CoinRect.rotation.eulerAngles.x - 90) < 5)
                    {
                        FrontImage.SetActive(false);
                        BackImage.SetActive(true);
                    }
                    else if (Mathf.Abs(CoinRect.rotation.eulerAngles.x - 270) < 5)
                    {
                        FrontImage.SetActive(true);
                        BackImage.SetActive(false);
                    }
                })
                .OnComplete(() =>
                {
                    transform.SetParent(ShowParent);
                    ShowSequence = DOTween.Sequence();
                    ShowSequence
                        .Append(CoinRect.DOAnchorPos(Vector2.zero, .5f, true)
                            .SetEase(Ease.OutQuad))
                        .Append(CoinRect.DOScale(Vector2.one * 2, .5f)
                            .SetEase(Ease.InBack))
                        .OnComplete(() =>
                        {
                            OnShowCompleteEvent?.Invoke(rotXTimes % 2 == 0);
                        });
                });
        }
    }
}