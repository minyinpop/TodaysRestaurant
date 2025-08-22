using UnityEngine;

namespace Battle_Management_System.Card_System.Battle_Card
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCardBase : MonoBehaviour, ICard, IBattleCard
    {
        [field: Header("Card Data")]
        [field: SerializeField] private CardSO CardData;
        
        private AnimationSystem AnimationSystem;

        private void Awake()
        {
            AnimationSystem = GetComponent<AnimationSystem>();
        }

        #region ICard
            public int GetDrawChance() => CardData.GetDrawChance();
        #endregion
    }
}