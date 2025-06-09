using UnityEngine.EventSystems;

namespace BATTLE.CARD.BASE
{
    internal interface ICard : IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnUse();
    }
}