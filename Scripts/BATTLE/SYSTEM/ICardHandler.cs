using UnityEngine.EventSystems;

namespace BATTLE.SYSTEM
{
    internal interface ICardHandler :
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        
    }
}