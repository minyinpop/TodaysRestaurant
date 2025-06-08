using BATTLE.CARD.STATE_MACHINE;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD.STATE
{
    internal class IdleState : ICardState
    {
        private GameObject Card { get; set; }
        private Tween RotateTween { get; set; }

        public void Enter(GameObject card)
        {
            Card = card;
            RotateTween?.Kill();
            RotateTween = Card.transform.DOLocalRotate(Vector3.forward * -5, 3, RotateMode.Fast)
                .From(Vector3.forward * 5)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void Exit()
        {
            RotateTween?.Kill();
            RotateTween = null;
            Card = null;
        }
    }
}