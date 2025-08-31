using UnityEngine;

namespace Battle_System.Object.Card.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Battle/Card", fileName = "New Name")]
    internal sealed class CardSO : ScriptableObject
    {
        [field: SerializeField, Range(0, 100)] private float DrawChance;

        public void GetDrawChance(out float GetDrawChance)
        {
            GetDrawChance = DrawChance;
        }
    }
}