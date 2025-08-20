using BATTLE.CARD_SYSTEM.CARD;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.SHOW_CARD_SYSTEM
{
    internal class ShowCardSystem : MonoBehaviour
    {
        [field: Header("Point")]
        [field: SerializeField] private CardSlot LeftPoint01;
        [field: SerializeField] private CardSlot LeftPoint02;
        [field: SerializeField] private CardSlot MiddlePoint;
        [field: SerializeField] private CardSlot RightPoint01;
        [field: SerializeField] private CardSlot RightPoint02;

        public void DrawCardWhenStartBattle(GameObject card)
        {
            if (RightPoint02.IsEmpty())
            {
                RightPoint02.AddCard(card);
                card.GetComponent<ICard>().OnDrawCardAndShow(RightPoint02);
            }
            else if (RightPoint01.IsEmpty())
            {
                RightPoint01.AddCard(card);
                card.GetComponent<ICard>().OnDrawCardAndShow(RightPoint01);
            }
            else if (MiddlePoint.IsEmpty())
            {
                MiddlePoint.AddCard(card);
                card.GetComponent<ICard>().OnDrawCardAndShow(MiddlePoint);
            }
            else if (LeftPoint02.IsEmpty())
            {
                LeftPoint02.AddCard(card);
                card.GetComponent<ICard>().OnDrawCardAndShow(LeftPoint02);
            }
            else if (LeftPoint01.IsEmpty())
            {
                LeftPoint01.AddCard(card);
                card.GetComponent<ICard>().OnDrawCardAndShow(LeftPoint01);
            }
        }
    }
}