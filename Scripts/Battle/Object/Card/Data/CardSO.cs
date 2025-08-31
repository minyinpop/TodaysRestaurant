using UnityEngine;

namespace Battle.Object.Card.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Battle/Card", fileName = "New Card")]
    internal sealed class CardSO : ScriptableObject
    {
        [field: Header("Chance")]
        [field: SerializeField, Range(0, 100)] private float DrawChance;

        public void GetDrawChance(out float DrawChance)
        {
            DrawChance = this.DrawChance;
        }
    }
}