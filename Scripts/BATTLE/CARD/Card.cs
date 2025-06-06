using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD
{
    internal class Card :
        MonoBehaviour, IDragHandler,
        IBeginDragHandler, IEndDragHandler,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        public static event System.Action<Card> BeginDragEvent;
        public static event System.Action EndDragEvent;
        
        #region Pointer Events
        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
        #endregion
    }
}