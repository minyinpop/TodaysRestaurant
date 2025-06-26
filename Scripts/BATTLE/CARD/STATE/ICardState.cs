using UnityEngine;

namespace BATTLE.CARD.STATE
{
    internal interface ICardState
    {
        public void Enter(GameObject card);
        public void Exit();

        public void OnPointerEnter();
        public void OnPointerExit();
        public void OnPointerClick();
    }
}