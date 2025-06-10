using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.COIN
{
    internal class Coin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private RectTransform Rect { get; set; }
        
        private bool CanClicked { get; set; } = true;
        
        private Sequence ThrowSequence { get; set; }
        private Tween ScaleTween { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanClicked) return;
            ScaleTween = Rect.DOScale(Vector3.one * 1.5f, .2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanClicked) return;
            ScaleTween = Rect.DOScale(Vector3.one, .2f);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanClicked) return;
            CanClicked = false;

            var randomXDistance = Random.Range(100, 801) * (Random.Range(-1, 2) == 0 ? -1 : 1);
            var randomYDistance = Random.Range(500, 801);

            var randomXRotation = Random.Range(3, 11) * 180;
            var randomYRotation = Random.Range(3, 11) * 180;
            var randomZRotation = Random.Range(3, 11) * Random.Range(1, 361);
            
            ThrowSequence = DOTween.Sequence();
            ThrowSequence
                .Append(Rect.DOScale(Vector3.one, 1)
                    .SetEase(Ease.InBack))
                .Join(Rect.DOAnchorPos(new Vector2(randomXDistance, randomYDistance), 1, true)
                    .SetEase(Ease.OutQuad))
                .Join(Rect.DORotate(new Vector3(randomXRotation, randomYRotation, randomZRotation), 1, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad));
        }
    }
}