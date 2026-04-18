using System;
using Common.Value.Type;
using UnityEngine;

namespace Common.Character
{
    [Serializable]
    public sealed class CharacterData
    {
        public CharacterType CharacterType { get; }

        public int Health { get; private set; }

        public float MoveSpeed { get; }
        
        public BattleCardType[] BattleCardTypes { get; }
        
        public ParticleSystem DeathVFX { get; }

        public CharacterData(CharacterType characterType, int health, float moveSpeed, BattleCardType[] battleCardTypes, ParticleSystem deathVFX)
        {
            CharacterType = characterType;
            Health = health;
            MoveSpeed = moveSpeed;
            BattleCardTypes = battleCardTypes;
            DeathVFX = deathVFX;
        }
        
        public void SubtractHealth(int damage, Action alive, Action dead)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(damage)} must be greater than 0.");
            }

            Health = Math.Max(Health - damage, 0);
            
            Debug.Log($"{CharacterType.ToString()} 受到了 {damage} 點傷害，當前血量為 {Health}。");

            if (Health > 0)
            {
                Debug.Log($"{CharacterType.ToString()} 還存活著。");
                
                alive.Invoke();
            }
            else
            {
                Debug.Log($"{CharacterType.ToString()} 已經死亡。");
                
                dead.Invoke();
            }
        }
    }
}