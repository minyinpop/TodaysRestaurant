using UnityEngine;

namespace Battle.Object.Card.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Battle/Card", fileName = "New Card")]
    internal sealed class CardSO : ScriptableObject
    {
        [field: SerializeField, Range(0, 100)] public float DrawChance;

        public void GetDrawChance(out float DrawChance)
        {
            DrawChance = this.DrawChance;
        }
    }
}