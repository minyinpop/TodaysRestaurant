using Common.Value;
using UnityEngine;

namespace Common.Data.Enemy
{
    [CreateAssetMenu(menuName = "Minyinpop/Enemy", fileName = "New Data")]
    public sealed class EnemySO : ScriptableObject
    {
        #region Information
            [field: Header("Information")]
            [field: SerializeField] private Sprite enemyImage;
                                    private Sprite EnemyImage => enemyImage;
        #endregion
        
        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private Health health;
                                    public Health Health => health;
            [field: SerializeField] private Damage damage;
                                    public Damage Damage => damage;
        #endregion
    }
}