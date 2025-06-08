using System.Collections;
using BATTLE.SYSTEM;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD
{
    internal class CardMove : MonoBehaviour, ICardHandler
    {
        [field: SerializeField] private Card Card { get; set; }
        
        private Tween MoveTween { get; set; }
        private Tween RotateTween { get; set; }
        private Tween ScaleTween { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ScaleTween?.Kill();
            ScaleTween = transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ScaleTween?.Kill();
            ScaleTween = transform.DOScale(new Vector3(1f, 1f, 1f), .2f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveTween?.Kill();
            MoveTween = transform.DOMove(eventData.position, .3f);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            MoveTween?.Kill();
            MoveTween = transform.DOLocalMove(Vector3.zero, .1f);
        }
    }
}