using System;
using Common.Value.Type;

namespace Common.Character
{
    [Serializable]
    public sealed class CharacterData
    {
        public string CharacterName { get; }

        public int Health { get; private set; }

        public float MoveSpeed { get; }
        
        public CardType[] CardTypes { get; }

        public CharacterData(string characterName, int health, float moveSpeed, CardType[] cardTypes)
        {
            CharacterName = characterName;
            Health = health;
            MoveSpeed = moveSpeed;
            CardTypes = cardTypes;
        }
        
        public void SubtractHealth(int damage, Action alive, Action dead)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(damage)} must be greater than 0.");
            }

            Health = Math.Max(Health - damage, 0);

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