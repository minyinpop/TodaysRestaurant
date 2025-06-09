using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.BASE
{
    internal abstract class CardBase : MonoBehaviour, ICard
    {
        [field: Header("Card Transform Settings")]
        [field: SerializeField] protected Transform CardSlotTrans { get; private set; }
        [field: SerializeField] protected Transform CardTrans { get; private set; }
        
        protected InputManager Input { get; set; }
        protected Vector2 MousePos => Input.Mouse.Position.ReadValue<Vector2>();
        
        protected bool IsDragging { get; set; }
        
        public abstract void OnPointerEnter(PointerEventData eventData);
        public abstract void OnPointerExit(PointerEventData eventData);
        public abstract void OnPointerDown(PointerEventData eventData);
        public abstract void OnPointerUp(PointerEventData eventData);
        public abstract void OnBeginDrag(PointerEventData eventData);
        public abstract void OnDrag(PointerEventData eventData);
        public abstract void OnEndDrag(PointerEventData eventData);
        public abstract void OnUse();
    }
}