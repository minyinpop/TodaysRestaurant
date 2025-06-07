using System.Collections.Generic;
using BATTLE.CARD.SYSTEM;
using SYSTEM;
using UnityEngine;

namespace BATTLE.CARD.MANAGER
{
    [RequireComponent(typeof(CardDragSystem))]
    [RequireComponent(typeof(CardSortingSystem))]
    internal class CardMainSystem : MonoBehaviour, ICardSystemHandler
    {
        [field: SerializeField] private CardDragSystem CardDragSystem { get; set; }
        [field: SerializeField] private CardSortingSystem CardSortingSystem { get; set; }
        [field: SerializeField] public List<GameObject> Slots { get; private set; }
        
        private InputManager Input { get; set; }
        public Vector2 MousePosition => Input.Mouse.Position.ReadValue<Vector2>();

        public GameObject DraggedCard { get; private set; }
        public Card DraggedCardComponent { get; private set; }
        
        private void Awake()
        {
            Input = InputSystem.Input;
        }

        private void OnEnable()
        {
            Card.DragEvent += Drag;
            Card.BeginDragEvent += BeginDrag;
            Card.EndDragEvent += EndDrag;
        }

        private void OnDisable()
        {
            Card.DragEvent -= Drag;
            Card.BeginDragEvent -= BeginDrag;
            Card.EndDragEvent -= EndDrag;
        }
        
        public void BeginDrag(Card draggedCard)
        {
            DraggedCard = draggedCard.gameObject;
            DraggedCardComponent = draggedCard;
            
            CardDragSystem.BeginDrag(DraggedCardComponent);
            CardSortingSystem.BeginDrag(DraggedCardComponent);
        }
        
        public void Drag()
        {
            CardDragSystem.Drag();
            CardSortingSystem.Drag();
        }

        public void EndDrag()
        {
            CardDragSystem.EndDrag();
            CardSortingSystem.EndDrag();
            
            DraggedCard.transform.localPosition = Vector3.zero;
            DraggedCard = null;
            DraggedCardComponent = null;
        }
    }
}