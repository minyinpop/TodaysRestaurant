using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CARD_SYSTEM.BATTLE_CARD_SYSTEM.POINTER_EVENT_SYSTEM
{
    internal class PointerEventSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;

        private Tween RotateTween;
        private Tween ScaleTween;

        private readonly System.Action onClick; // TODO 我剛剛把這裏弄完，接下來把這個完成
        public PointerEventSystem(System.Action OnClick) => onClick = OnClick;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            KillTween();
            ScaleTween = ScaleUpWhenCursorEnter();
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            KillTween();
            ScaleTween = ScaleDownWhenCursorExit();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            KillTween();
            RotateTween = ShakeWhenClicked();
        }
        
        #region Tween
            private Tween ScaleUpWhenCursorEnter()
            {
                return Rect
                    .DOScale(Vector2.one * 1.25f, .25f)
                    .SetEase(Ease.OutQuart);
            }

            private Tween ScaleDownWhenCursorExit()
            {
                return Rect
                    .DOScale(Vector2.one, .25f)
                    .SetEase(Ease.OutQuart);
            }
            
            
            
            
            
            private void PopUpOrDownWhenClicked()
            {
            }
            
            private Tween ShakeWhenClicked()
            {
                return Rect
                    .DOShakeRotation(.25f, Vector3.forward * 10, 10, 90, true, ShakeRandomnessMode.Harmonic)
                    .SetEase(Ease.Linear)
                    .OnKill(() => Rect.eulerAngles = Vector3.zero);
            }
            
            
            
            
            
            private void KillTween()
            {
                RotateTween?.Kill();
                ScaleTween?.Kill();
                
                RotateTween = null;
                ScaleTween = null;
            }
        #endregion
    }
}