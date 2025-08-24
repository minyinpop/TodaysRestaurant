using Battle_Management_System.Card_System.Base;
using Battle_Management_System.Card_System.Data;
using UnityEngine;

namespace Battle_Management_System.Card_System.Type.Battle.Base
{
    internal abstract class BattleCard : MonoBehaviour, ICard, IBattleCard
    {
        [field: Header("Card Data")]
        [field: SerializeField] private CardData CardData;
        
        public void GetDrawChance(out float outChance)
        {
            CardData.GetDrawChance(out outChance);
        }
    }
}