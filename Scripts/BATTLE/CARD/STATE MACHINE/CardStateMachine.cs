namespace BATTLE.CARD.STATE_MACHINE
{
    internal class CardStateMachine
    {
        private ICardState CurrentState;

        /// <summary>
        /// 退出當前的狀態
        /// 進到下一個狀態
        /// </summary>
        /// <param name="newState"> 下一個狀態 </param>
        public void ChangeState(ICardState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
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