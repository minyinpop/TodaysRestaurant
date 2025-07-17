using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UTILITY;

namespace BATTLE.SELECTION_INITIATIVE_SYSTEM
{
    internal class InitiativeCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private RectTransform RectTransform;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="posMult"></param>
        public void MoveToReadyPos(Rect rect, AnchorsMult posMult)
        {
            DOTween.Sequence()
                .Append(RectTransform
                    .DOAnchorPos(new Vector2(rect.width * posMult.GetRandomXMult(), rect.height * posMult.GetRandomYMult()), 1)
                    .SetEase(Ease.OutBack));
            
            // TODO 07.18 繼續撰寫硬幣到 Ready 點後，要幹嘛的程式碼
        }
    }
}