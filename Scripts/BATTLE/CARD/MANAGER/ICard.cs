using UnityEngine.EventSystems;

namespace BATTLE.CARD.MANAGER
{
    internal interface ICard : IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        
    }
}