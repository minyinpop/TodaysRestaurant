using Common.Enemy.Enemy_Object;
using Common.Value;
using UnityEngine;

namespace Common.Enemy.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Enemy", fileName = "New Data")]
    public sealed class EnemySO : ScriptableObject
    {
        #region Information
            [field: Header("Information")]
            [field: SerializeField] private Sprite enemyImage;
                                    public Sprite EnemyImage => enemyImage;
            [field: SerializeField] private EnemyObject enemyObject;
                                    public EnemyObject EnemyObject => enemyObject;
        #endregion
        
        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private int health;
                                    public int Health => health;
            [field: SerializeField] private Damage damage;
                                    public Damage Damage => damage;
        #endregion

        private void OnValidate()
        {
            #region Information
                if (enemyImage == null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(enemyImage)} cannot be null.");
                    return;
                }
            #endregion

            #region Attribute
                if (health < 0)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(health)} cannot be negative.");
                    return;
                }

                if (damage.BasicDamage < 0)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(damage.BasicDamage)} cannot be negative.");
                }
            #endregion
        }
    }
}