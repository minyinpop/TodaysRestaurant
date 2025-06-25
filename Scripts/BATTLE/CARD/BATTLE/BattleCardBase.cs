using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.BATTLE
{
    internal abstract class BattleCardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [field: SerializeField] private RectTransform SelectionOrderParent { get; set; }
        private GameObject SelectionOrderObject { get; set; }
        
        // Components
        private RectTransform RectTransform { get; set; }
        
        // Values
        private bool Selected { get; set; }
        
        // Broadcasts
        public static event System.Action<BattleCardBase> SelectedEvent;
        public static event System.Action<BattleCardBase> DeselectEvent;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            RectTransform
                .DOScale(Vector2.one * 1.2f, .2f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RectTransform
                .DOScale(Vector2.one, .2f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // TODO 要先確認是否還有空間可以選取卡片，數量低於 3 後再往下執行
            
            Selected = !Selected;
            var targetPos = Selected
                ? RectTransform.anchoredPosition + Vector2.up * 150
                : RectTransform.anchoredPosition - Vector2.up * 150;

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    if (Selected)
                        SelectedEvent?.Invoke(this);
                    else
                        DeselectEvent?.Invoke(this);
                })
                .Append(RectTransform
                    .DOAnchorPos(targetPos, .5f, true)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOShakeRotation(.2f, Vector3.forward * 10, 1, 90, true, ShakeRandomnessMode.Harmonic)
                    .SetEase(Ease.OutQuad));
        }

        public void SetSelectionOrder(GameObject selectionOrderObject)
        {
            if (SelectionOrderObject is not null)
            {
                Destroy(SelectionOrderObject);
                SelectionOrderObject = null;
            }

            SelectionOrderObject = Instantiate(selectionOrderObject, SelectionOrderParent);
        }
        
        public void RemoveSelectionOrder()
        {
            Destroy(SelectionOrderObject);
            SelectionOrderObject = null;
        }
    }
}