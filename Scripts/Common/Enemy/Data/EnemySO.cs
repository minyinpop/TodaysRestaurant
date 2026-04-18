using Audio_System.Data;
using Common.Enemy.Enemy_Object;
using Common.Value.Type;
using UnityEngine;

namespace Common.Enemy.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Enemy", fileName = "New Data")]
    public sealed class EnemySO : ScriptableObject
    {
        [field: Header("敵人圖片")]
        [field: SerializeField] private Sprite enemyImage;
                                public Sprite EnemyImage => enemyImage;
                                
        [field: Header("敵人的預製件")]
        [field: SerializeField] private EnemyObject enemyObject;
                                public EnemyObject EnemyObject => enemyObject;
    
        [field: Header("血量")]
        [field: SerializeField] private int health;
                                public int Health => health;
                                
        [field: Header("攻擊類型")]
        [field: SerializeField] private AttackType attackType;
                                public AttackType AttackType => attackType;
        
        [field: Header("攻擊次數")]
        [field: SerializeField] private int attackTime;
                                public int AttackTime => attackTime;
        
        [field: Header("傷害")]
        [field: SerializeField] private int damage;
                                public int Damage => damage;
        
        [field: Header("攻擊音效")]
        [field: SerializeField] private PlaySFXData attackSFX;
                                public PlaySFXData AttackSFX => attackSFX;
        
        [field: Header("攻擊特效")]
        [field: SerializeField] private ParticleSystem attackVFX;
                                public ParticleSystem AttackVFX => attackVFX;
        
        [field: Header("死亡特效")]
        [field: SerializeField] private ParticleSystem deathVFX;
                                public ParticleSystem DeathVFX => deathVFX;

        private void OnValidate()
        {
            #region Attribute
                if (health < 0)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(health)} cannot be negative.");
                    return;
                }

                if (damage < 0)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(damage)} cannot be negative.");
                }
            #endregion
        }
    }
}