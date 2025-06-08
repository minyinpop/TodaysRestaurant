using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD
{
    internal class CardMove : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Card Card { get; set; }
        
        private Tween MoveTween { get; set; }
        private Tween ScaleTween { get; set; }

        public void OnPointerEnter()
        {
            ScaleTween?.Kill();
            ScaleTween = Card.HandCard.transform.DOScale(1.2f, .2f);
        }

        public void OnPointerExit()
        {
            ScaleTween?.Kill();
            ScaleTween = Card.HandCard.transform.DOScale(1f, .2f);
        }

        public void OnDrag()
        {
            MoveTween?.Kill();
            MoveTween = Card.HandCard.transform.DOMove(Card.MousePos, .3f);
        }

        public void OnEndDrag()
        {
            MoveTween?.Kill();
            MoveTween = Card.HandCard.transform.DOMove(Card.HandCardSlot.transform.position, .3f);
        }
    }
}