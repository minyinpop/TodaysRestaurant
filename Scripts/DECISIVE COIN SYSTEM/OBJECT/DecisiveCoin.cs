using DG.Tweening;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM.OBJECT
{
    internal class DecisiveCoin : MonoBehaviour
    {
        [field: SerializeField] private RectTransform ShowPoint;
        [field: SerializeField] private RectTransform LandPoint;
        [field: SerializeField] private RectTransform TossPoint;
        [field: SerializeField] private RectTransform PreparePoint;
        
        private Tween MoveTween;
        private Tween RotateTween;
        private Tween ScaleTween;
        
        public void MoveToTossPoint()
        {
            
        }

        private void KillTween()
        {
        }
    }
}