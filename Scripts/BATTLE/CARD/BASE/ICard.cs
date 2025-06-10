using UnityEngine.EventSystems;

namespace BATTLE.CARD.BASE
{
    internal interface ICard : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public void OnChoose();
    }
}