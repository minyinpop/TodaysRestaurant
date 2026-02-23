using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Common.Character
{
    [CreateAssetMenu(menuName = "Minyinpop/Character", fileName = "New Data")]
    public sealed class CharacterSO : ScriptableObject
    {
        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private int health;
                                    public int Health => health;
            [field: SerializeField] private Damage damage;
                                    public Damage Damage => damage;
        #endregion
        
        #region Battle
            [field: Header("Card Type")]
            [field: SerializeField] private CardType[] cardTypes;
                                    public CardType[] CardTypes => cardTypes;
        #endregion
    }
}