using Common.Value.Type;
using UnityEngine;

namespace Common.Character
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Deck", fileName = "New Data")]
    public sealed class CharacterSO : ScriptableObject
    {
        [field: Header("Type")]
        [field: SerializeField] private CharacterType characterType;
                                public CharacterType CharacterType => characterType;
        
        [field: Header("Health")]
        [field: SerializeField] private int maxHealth;
                                public int MaxHealth => maxHealth;
                                
        [field: Header("Move Speed")]
        [field: SerializeField] private float moveSpeed;
                                public float MoveSpeed => moveSpeed;
        
        [field: Header("Card Type")]
        [field: SerializeField] private CardType[] cardTypes;
                                public CardType[] CardTypes => cardTypes;
        
        /*
        
        */
    }
}