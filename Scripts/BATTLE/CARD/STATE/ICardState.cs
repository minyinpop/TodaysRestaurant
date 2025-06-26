using BATTLE.CARD.BATTLE;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.STATE
{
    internal interface ICardState : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public void Enter(BattleCardBase card);
        public void Exit();
    }
}