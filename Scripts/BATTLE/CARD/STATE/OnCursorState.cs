using BATTLE.CARD.STATE_MACHINE;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD.STATE
{
    internal class OnCursorState : ICardState
    {
        private GameObject Card { get; set; }
        
        private Tween ShakePositionTween { get; set; }
        private Tween RotationTween { get; set; }
        
        public void Enter(GameObject card)
        {
            Card = card;
            ShakePositionTween?.Kill();
            RotationTween?.Kill();
            ShakePositionTween = Card.transform.DOShakePosition(.3f, 15, 1, 90, true, true, ShakeRandomnessMode.Harmonic);
            RotationTween = Card.transform.DORotate(Vector3.zero, .2f, RotateMode.Fast);
        }

        public void Exit()
        {
            ShakePositionTween?.Kill();
            RotationTween?.Kill();
            if (Card is not null)
            {
                Card.transform.localPosition = Vector3.zero;
                Card.transform.eulerAngles = Vector3.zero;
            }

            ShakePositionTween = null;
            RotationTween = null;
            Card = null;
        }
    }
}