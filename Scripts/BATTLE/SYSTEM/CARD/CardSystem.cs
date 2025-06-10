using System.Collections.Generic;
using BATTLE.CARD.BASE;
using UnityEngine;

namespace BATTLE.SYSTEM.CARD
{
    internal class CardSystem : MonoBehaviour
    {
        [field: Header("Settings")]
        [field: SerializeField] private CardLayoutSettings CardLayoutSettings { get; set; }

        private List<ICard> ChooseCards { get; set; } = new();

        private void LateUpdate()
        {
            CardLayoutSettings.ArrangeCardsByCurve();
        }
    }
}