using BATTLE.CARD.SYSTEM;
using SYSTEM;
using UnityEngine;

namespace BATTLE.CARD.MANAGER
{
    [RequireComponent(typeof(CardDragSystem))]
    [RequireComponent(typeof(CardSortingSystem))]
    internal class CardMainSystem : MonoBehaviour, ICardSystemHandler
    {
        [field: Header("System Components")]
        [field: SerializeField] private CardDragSystem CardDragSystem { get; set; }
        [field: SerializeField] private CardSortingSystem CardSortingSystem { get; set; }
        
        private InputManager Input { get; set; }
        public Vector2 MousePosition => Input.Mouse.Position.ReadValue<Vector2>();

        private GameObject DraggedCard { get; set; }
        private Card DraggedCardComponent { get; set; }
        
        private void Awake()
        {
            Input = InputSystem.Input;
        }

        private void OnEnable()
        {
            Card.BeginDragEvent += BeginDrag;
            Card.EndDragEvent += EndDrag;
        }

        private void OnDisable()
        {
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