using UnityEngine;

namespace Battle_Management.Card.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Battle/Card Data", fileName = "New Card Data")]
    internal class CardSO : ScriptableObject
    {
        [field: Header("Chance")]
        [field: SerializeField, Range(0f, 100f)] private float DrawChance;

        public void GetDrawChance(out float chance)
        {
            chance = DrawChance;
        }
    }
}