using BATTLE.CARD.STATE_MACHINE;
using BATTLE.CARD.STATE_MACHINE.STATE;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD
{
    internal abstract class CardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private CardStateMachine StateMachine = new();

        private void Start()
        {
            StateMachine.ChangeState(new DeselectedState(), this);
        }
        
        
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            StateMachine.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StateMachine.OnPointerExit();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            StateMachine.OnPointerClick();
        }
    }
}