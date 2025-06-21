using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.BATTLE
{
    internal abstract class BattleCardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private RectTransform SelectionOrderParent { get; set; }
        
        // Components
        private RectTransform RectTransform { get; set; }
        
        // Values
        private bool Selected { get; set; }
        private Vector2 OriginalSizeDelta { get; set; }
        
        // Broadcasts
        public static event System.Action<BattleCardBase> OnCardSelectedEvent;
        

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            OriginalSizeDelta = RectTransform.sizeDelta;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            RectTransform.DOSizeDelta(OriginalSizeDelta * 1.25f, .2f).SetEase(Ease.OutQuad);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RectTransform.DOSizeDelta(OriginalSizeDelta, .2f).SetEase(Ease.OutQuad);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}