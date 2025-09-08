using UnityEngine;

namespace Data.Card.Battle
{
    [CreateAssetMenu(menuName = "Minyinpop/Card/Battle", fileName = "New Data")]
    internal sealed class BattleCardSO : ScriptableObject
    {
        [field: Header("Chance")]
        [field: SerializeField, Range(0, 100)] private float DrawChance;

        /// <summary>
        /// 獲取卡片被抽到的機率
        /// </summary>
        /// <paramref name="chance">回傳被抽到的機率</paramref>
        public void GetDrawChance(out float chance)
        {
            chance = DrawChance;
        }
    }
}