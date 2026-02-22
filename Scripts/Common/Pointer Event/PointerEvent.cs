using UnityEngine;
using UnityEngine.EventSystems;

namespace Common.Pointer_Event
{
    public abstract class PointerEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public void OnPointerEnter(PointerEventData eventData) => OnPointerEnter();
        public void OnPointerExit(PointerEventData eventData) => OnPointerExit();
        public void OnPointerClick(PointerEventData eventData) => OnPointerClick();

        protected virtual void OnPointerEnter() { }
        protected virtual void OnPointerExit() { }
        protected virtual void OnPointerClick() { }
    }
}