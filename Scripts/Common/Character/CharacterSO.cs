using Common.Value.Type;
using UnityEngine;

namespace Common.Character
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Deck", fileName = "New Data")]
    public sealed class CharacterSO : ScriptableObject
    {
        [field: Header("角色類型")]
        [field: SerializeField] private CharacterType characterType;
                                public CharacterType CharacterType => characterType;
        
        [field: Header("最大血量")]
        [field: SerializeField] private int maxHealth;
                                public int MaxHealth => maxHealth;
                                
        [field: Header("移動速度")]
        [field: SerializeField] private float moveSpeed;
                                public float MoveSpeed => moveSpeed;
        
        [field: Header("戰鬥卡片類型")]
        [field: SerializeField] private BattleCardType[] battleCardTypes;
                                public BattleCardType[] BattleCardTypes => battleCardTypes;
                                
        [field: Header("死亡特效")]
        [field: SerializeField] private ParticleSystem deathVFX;
                                public ParticleSystem DeathVFX => deathVFX;
    }
}