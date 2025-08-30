using UnityEngine;

namespace Battle_System.Card_System.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Battle System/Card Data", fileName = "New Card Data")]
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