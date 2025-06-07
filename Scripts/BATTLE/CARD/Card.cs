using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD
{
    internal class Card :
        MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // public static event System.Action PointerEnterEvent;
        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        // public static event System.Action PointerExitEvent;
        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        // public static event System.Action PointerDownEvent;
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        // public static event System.Action PointerUpEvent;
        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
        
        public static event System.Action<Card> BeginDragEvent;
        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(this);
        }
        
        public static event System.Action DragEvent;
        public void OnDrag(PointerEventData eventData)
        {
            DragEvent?.Invoke();
        }

        public static event System.Action EndDragEvent;
        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke();
        }
    }
}