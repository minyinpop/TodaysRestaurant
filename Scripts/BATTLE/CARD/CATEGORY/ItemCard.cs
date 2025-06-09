using BATTLE.CARD.BASE;
using BATTLE.CARD.STATE_MACHINE;
using BATTLE.CARD.STATE;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.Category
{
    internal class ItemCard : MonoBehaviour, ICard
    {
        [field: SerializeField] private GameObject Card { get; set; }
        
        private CardStateMachine CardStateMachine { get; set; }

        private void Awake()
        {
            CardStateMachine = new CardStateMachine();
            CardStateMachine.SetState(new IdleState(), Card);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CardStateMachine.ChangeState(new OnCursorState(), Card);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CardStateMachine.ChangeState(new IdleState(), Card);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CardStateMachine.ChangeState(new ClickState(), Card);
        }
    }
}