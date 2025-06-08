using UnityEngine.EventSystems;

namespace BATTLE.CARD
{
    internal interface ICardHandler :
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        
    }
}