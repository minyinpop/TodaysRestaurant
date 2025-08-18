using BATTLE.CARD_SYSTEM.CARD;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.DRAW_CARD_SYSTEM
{
    internal class DrawCardSystem : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private RectTransform LeftPoint01;
        [field: SerializeField] private RectTransform LeftPoint02;
        [field: SerializeField] private RectTransform MiddlePoint;
        [field: SerializeField] private RectTransform RightPoint01;
        [field: SerializeField] private RectTransform RightPoint02;

        public void DrawOneCard(GameObject card)
        {
            card.GetComponent<ICard>().OnDrawCardAndShow(MiddlePoint);
        }
    }
}