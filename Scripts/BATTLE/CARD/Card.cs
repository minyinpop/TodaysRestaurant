using INPUT.SYSTEM;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD
{
    [RequireComponent(typeof(CardMove))]
    internal class Card : MonoBehaviour, ICardHandler
    {
        [field: Header("Component")]
        [field: SerializeField] private CardMove CardMove { get; set; }
        
        [field: Header("GameObject")]
        [field: SerializeField] public GameObject HandCardSlot { get; private set; }
        [field: SerializeField] public GameObject HandCard { get; private set; }
        
        private InputManager Input { get; set; }
        public Vector2 MousePos => Input.Mouse.Position.ReadValue<Vector2>();

        private void Awake()
        {
            Input = InputSystem.Input;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CardMove.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CardMove.OnPointerExit();
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
            CardMove.OnDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            CardMove.OnEndDrag();
        }
    }
}