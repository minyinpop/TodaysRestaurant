using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BATTLE.COIN
{
    internal class Coin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: Header("Coin Transform")]
        [field: SerializeField] private RectTransform CoinRect { get; set; }
        
        [field: Header("Coin Image")]
        [field: SerializeField] private GameObject FrontImage { get; set; }
        [field: SerializeField] private GameObject BackImage { get; set; }
        
        private bool CanClicked { get; set; } = true;
        
        private Sequence ThrowSequence { get; set; }
        private Tween ScaleTween { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanClicked) return;
            ScaleTween = CoinRect.DOScale(Vector3.one * 1.5f, .2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanClicked) return;
            ScaleTween = CoinRect.DOScale(Vector3.one, .2f);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanClicked) return;
            CanClicked = false;

            var randomXDistance = Random.Range(100, 801) * (Random.Range(-1, 2) == 0 ? -1 : 1);
            var randomYDistance = Random.Range(500, 801);

            var randomXRotation = Random.Range(12, 16) * 180;
            var randomZRotation = Random.Range(5, 8) * Random.Range(1, 361);
            
            ThrowSequence = DOTween.Sequence();
            ThrowSequence
                .Append(CoinRect.DOScale(Vector3.one, 1)
                    .SetEase(Ease.InBack))
                .Join(CoinRect.DOAnchorPos(new Vector2(randomXDistance, randomYDistance), 2, true)
                    .SetEase(Ease.OutQuad))
                .Join(CoinRect.DORotate(new Vector3(randomXRotation, 0, randomZRotation), 2,
                        RotateMode.FastBeyond360)
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
                    // TODO 硬幣翻轉動畫播完後，要告訴玩家哪一面朝上
                });
        }
    }
}