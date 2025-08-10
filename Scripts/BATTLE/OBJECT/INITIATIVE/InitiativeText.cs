using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BATTLE.OBJECT.INITIATIVE
{
    internal class InitiativeText : MonoBehaviour
    {
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private TextMeshProUGUI TextTMP;

        private Tween ScaleTween;

        public Tween ShowText(Color textColor, string content)
        {
            KillTween();
            
            gameObject.SetActive(true);
            
            TextTMP.color = textColor;
            TextTMP.text = content;
            
            ScaleTween = Rect
                .DOScale(Vector2.one * 1, .5f)
                .SetEase(Ease.OutQuad);
            return ScaleTween;
        }

        public Tween HideText()
        {
            KillTween();
            
            ScaleTween = Rect
                .DOScale(Vector2.zero, .5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => { gameObject.SetActive(false); });
            return ScaleTween;
        }

        private void KillTween()
        {
            ScaleTween?.Kill();
            ScaleTween = null;
        }
    }
}