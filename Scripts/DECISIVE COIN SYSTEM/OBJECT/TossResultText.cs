using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM.OBJECT
{
    internal class TossResultText : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private GameObject Text;
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private TextMeshProUGUI TextTMP;
        
        private Tween ScaleTween;

        public Tween ShowText()
        {
            KillTween();

            return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    TextTMP.text = "請修改文字";
                    Text.SetActive(true);
                })
                .Append(ScaleTween = ScaleShowTween());
        }
        
        public Tween HideText()
        {
            KillTween();
            ScaleTween = ScaleHideTween().OnComplete(() => Text.SetActive(false));
            return ScaleTween;
        }

        #region Tween
            private Tween ScaleShowTween()
            {
                return Rect
                    .DOScale(Vector2.one, .5f)
                    .SetEase(Ease.OutBack);
            }
        
            private Tween ScaleHideTween()
            {
                return Rect
                    .DOScale(Vector2.zero, .5f)
                    .SetEase(Ease.InBack);
            }
            
            private void KillTween()
            {
                ScaleTween?.Kill();
                ScaleTween = null;
            }
        #endregion
    }
}