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
        public static event System.Func<BattleCardBase, GameObject> SelectedEvent;
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
            Selected = !Selected;
            var targetPos = Selected
                ? RectTransform.anchoredPosition + Vector2.up * 150
                : RectTransform.anchoredPosition - Vector2.down * 150;

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    if (Selected)
                        SetSelectionOrder(SelectedEvent?.Invoke(this));
                    else
                    {
                        DeselectEvent?.Invoke(this);
                        RemoveSelectionOrder();
                    }
                })
                .Append(RectTransform
                    .DOAnchorPos(targetPos, .5f, true)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOShakeRotation(.2f, Vector3.forward * 10, 1, 90, true, ShakeRandomnessMode.Harmonic)
                    .SetEase(Ease.OutQuad));
        }

        private void SetSelectionOrder(GameObject selectionOrderObject)
        {
            if (selectionOrderObject is null) return;
            SelectionOrderObject = Instantiate(selectionOrderObject, SelectionOrderParent);
        }
        
        private void RemoveSelectionOrder()
        {
            Destroy(SelectionOrderObject);
            SelectionOrderObject = null;
        }
    }
}