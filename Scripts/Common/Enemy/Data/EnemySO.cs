using Common.Enemy.Enemy_Object;
using Common.Value.Type;
using UnityEngine;

namespace Common.Enemy.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Enemy", fileName = "New Data")]
    public sealed class EnemySO : ScriptableObject
    {
        [field: Header("敵人資料")]
        [field: SerializeField] private Sprite enemyImage;
                                public Sprite EnemyImage => enemyImage;
        [field: SerializeField] private EnemyObject enemyObject;
                                public EnemyObject EnemyObject => enemyObject;
    
        [field: Header("屬性資料")]
        [field: SerializeField] private int health;
                                public int Health => health;
        [field: SerializeField] private AttackType attackType;
                                public AttackType AttackType => attackType;
        [field: SerializeField] private int damage;
                                public int Damage => damage;

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