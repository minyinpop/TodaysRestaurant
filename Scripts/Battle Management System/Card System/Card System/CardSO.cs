using UnityEngine;

namespace Battle_Management_System.Card_System.Card_System
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/New Card", fileName = "New Name", order = 1)]
    internal class CardSO : ScriptableObject
    {
        [field: SerializeField, Range(1, 100)] private int DrawChance;
        public int GetDrawChance() => DrawChance;
    }
}