using UnityEngine;

namespace Battle.Card.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Battle/Battle Card", fileName = "New Name")]
    internal sealed class CardSO : ScriptableObject
    {
        [field: Header("Chance")]
        [field: SerializeField, Range(0, 100)] private float DrawChance;
        
        public void GetDrawChance(out float Chance)
        {
            Chance = DrawChance;
        }
    }
}