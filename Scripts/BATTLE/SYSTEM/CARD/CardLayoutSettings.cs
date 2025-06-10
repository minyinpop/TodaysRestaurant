using UnityEngine;

namespace BATTLE.SYSTEM.CARD
{
    [System.Serializable]
    internal class CardLayoutSettings
    {
        [field: Header("Settings")]
        [field: SerializeField] public AnimationCurve Curve { get; set; }
        [field: SerializeField] public RectTransform[] Cards { get; set; }

        public void Update() => ArrangeCardsByCurve();
        
        private void ArrangeCardsByCurve()
        {
            var cardCount = Cards.Length;
            for (var i = 0; i < cardCount; i++)
            {
                var t = (float)i / (cardCount - 1);
                var yOffset = Curve.Evaluate(t);
                
                Vector3 position = Cards[i].anchoredPosition;
                position.y = yOffset;
                Cards[i].anchoredPosition = position;
            }
        }
    }
}