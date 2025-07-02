namespace BATTLE.CARD.STATE_MACHINE
{
    internal class CardStateMachine
    {
        private ICardState CurrentState;

        /// <summary>
        /// 離開當前狀態並進入到下個狀態
        /// </summary>
        /// <param name="newState"> 下個狀態 </param>
        /// <param name="card"> 當前互動的卡片 </param>
        public void ChangeState(ICardState newState, CardBase card)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter(card);
        }
        
        public void OnPointerEnter()
        {
            CurrentState?.OnPointerEnter();
        }
        
        public void OnPointerExit()
        {
            CurrentState?.OnPointerExit();
        }
        public void OnPointerClick()
        {
            CurrentState?.OnPointerClick();
        }
    }
}