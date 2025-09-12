using UnityEngine;

namespace Data.Card.Battle
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/Battle", fileName = "New Data")]
    internal sealed class BattleCardSO : ScriptableObject
    {
        [field: Header("Chance")]
        [field: SerializeField, Range(0, 100)] public float DrawChance { get; private set; }
    }
}