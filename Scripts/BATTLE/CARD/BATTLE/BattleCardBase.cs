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
        
        // Broadcasts
        public static event System.Action<BattleCardBase> OnCardSelectedEvent;
        

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            RectTransform
                .DOScale(Vector2.one * 1.25f, .2f)
                .SetEase(Ease.OutSine);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RectTransform
                .DOScale(Vector2.one, .2f)
                .SetEase(Ease.OutSine);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Selected = !Selected;
            var targetPos = Selected ? RectTransform.anchoredPosition + Vector2.up * 200 : RectTransform.anchoredPosition - Vector2.up * 200;

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    // TODO 發送廣播給 Manager，讓它生成玩家點擊卡片後，所顯示的先後順序的 Prefab
                })
                .Join(RectTransform
                    .DOAnchorPos(targetPos, .5f, true)
                    .SetEase(Ease.OutSine))
                .Join(RectTransform
                    .DOShakeRotation(.2f, Vector3.forward * 10, 1, 90, true, ShakeRandomnessMode.Harmonic)
                    .SetEase(Ease.OutSine));
        }
    }
}