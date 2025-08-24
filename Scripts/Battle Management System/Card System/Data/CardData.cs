using UnityEngine;

namespace Battle_Management_System.Card_System.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Card Data", fileName = "New Data", order = 1)]
    internal class CardData : ScriptableObject
    {
        [field: Header("Chance")]
        [field: SerializeField, Range(0, 100)] private float DrawChance;

        public void GetDrawChance(out float outChance)
        {
            outChance = DrawChance;
        }
    }
}