using UnityEngine;

namespace BATTLE.CARD
{
    [RequireComponent(typeof(CardEvent))]
    [RequireComponent(typeof(CardMove))]
    internal class Card : MonoBehaviour
    {
        [field: SerializeField] private CardEvent CardEvent { get; set; }
        [field: SerializeField] private CardMove CardMove { get; set; }
    }
}