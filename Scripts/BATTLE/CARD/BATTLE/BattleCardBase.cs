using BATTLE.CARD.STATE;
using BATTLE.CARD.STATE.TYPE;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.BATTLE
{
    internal abstract class BattleCardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private RectTransform Rect { get; set; }
        
        private CardStateMachine CardStateMachine { get; set; }
        
        private void Awake()
        {
            CardStateMachine = new CardStateMachine();
        }

        private void Start()
        {
            CardStateMachine.ChangeState(new DeselectedState(), gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CardStateMachine.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CardStateMachine.OnPointerExit();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            CardStateMachine.OnPointerClick();
        }

        public void ChangeState(ICardState newState)
        {
            CardStateMachine.ChangeState(newState, gameObject);
        }
    }
}