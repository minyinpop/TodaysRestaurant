using System;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Common.Player.Child.Player_Character
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Deck", fileName = "New Data")]
    public sealed class PlayerCharacterSO : ScriptableObject
    {
        [field: Header("Health")]
        [field: SerializeField] private int maxHealth;
                                public int MaxHealth => maxHealth;
                                public int Health { get; private set; }
                                
        [field: Header("Damage")]
        [field: SerializeField] private Damage damage;
                                public Damage Damage => damage;
                                
        [field: Header("Move Speed")]
        [field: SerializeField] private float moveSpeed;
                                public float MoveSpeed => moveSpeed;
        
        [field: Header("Card Type")]
        [field: SerializeField] private CardType[] cardTypes;
                                public CardType[] CardTypes => cardTypes;

        private void OnValidate()
        {
            Health = maxHealth;
        }
        
        public void SubtractHealth(int damage, Action alive, Action dead)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(damage)} must be greater than 0.");
            }

            Health = Mathf.Max(Health - damage, 0);

            if (Health > 0)
            {
                alive.Invoke();
            }
            else
            {
                dead.Invoke();
            }
        }
    }
}