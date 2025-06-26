using BATTLE.CARD.STATE;
using BATTLE.CARD.STATE.TYPE;
using UnityEngine;

namespace BATTLE.CARD.BATTLE
{
    internal abstract class BattleCardBase : MonoBehaviour
    {
        [field: SerializeField] private RectTransform Rect { get; set; }
        
        private CardStateMachine CardStateMachine { get; set; }
        
        private void Awake()
        {
            CardStateMachine = new CardStateMachine();
        }

        private void Start()
        {
            CardStateMachine.ChangeState(new DeselectedState(), this);
        }

        public void OnPointerEnter()
        {
            
        }

        public void OnPointerExit()
        {
            
        }
        
        public void OnPointerClick()
        {
            
        }
    }
}