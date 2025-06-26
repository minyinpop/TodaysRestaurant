using BATTLE.CARD.BATTLE;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD.STATE.TYPE
{
    internal class DeselectedState : ICardState
    {
        private GameObject Card { get; set; }
        private RectTransform CardRect { get; set; }
        private BattleCardBase CardBase { get; set; }
        
        public void Enter(GameObject card)
        {
            Card = card;
            CardRect = card.GetComponent<RectTransform>();
            CardBase = card.GetComponent<BattleCardBase>();
        }
        
        public void Exit()
        {
            
        }

        public void OnPointerEnter()
        {
            CardRect
                .DOScale(Vector2.one * 1.25f, .2f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerExit()
        {
            CardRect
                .DOScale(Vector2.one, .2f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerClick()
        {
            CardBase.ChangeState(new SelectedState());
            
            DOTween.Sequence()
                .Append(CardRect
                    .DOAnchorPos(CardRect.anchoredPosition + Vector2.up * 200, .5f)
                    .SetEase(Ease.OutQuad))
                .Join(CardRect
                    .DOShakeRotation(.2f, Vector3.forward * 10, 1, 90, true, ShakeRandomnessMode.Harmonic)
                    .SetEase(Ease.OutQuad));
        }
    }
}