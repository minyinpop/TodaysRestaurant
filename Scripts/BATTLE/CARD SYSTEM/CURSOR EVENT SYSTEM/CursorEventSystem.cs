using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD_SYSTEM.CURSOR_EVENT_SYSTEM
{
    internal class CursorEventSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event System.Action OnCursorEnter;
        public event System.Action OnCursorExit;
        public event System.Action OnCursorClick;
        
        public void OnPointerEnter(PointerEventData eventData) => OnCursorEnter?.Invoke();
        public void OnPointerExit(PointerEventData eventData) => OnCursorExit?.Invoke();
        public void OnPointerClick(PointerEventData eventData) => OnCursorClick?.Invoke();
    }
}