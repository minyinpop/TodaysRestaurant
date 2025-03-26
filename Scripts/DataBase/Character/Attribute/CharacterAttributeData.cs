using UnityEngine;

namespace DataBase.Character.Attribute
{
    /// <summary>
    /// 用來儲存角色的屬性以及狀態的資料庫。
    /// </summary>
    [CreateAssetMenu(fileName = "New Character Attribute Data", menuName = "Character Data/Attribute", order = 1)]
    public class CharacterAttributeData : ScriptableObject
    {
        [field: Header("移動設定"), Tooltip("玩家是否可以移動。"), SerializeField]
        public bool Moveable { get; private set; }
        
        [field: Tooltip("- 玩家的移動速度。\n- 使用 Rigidbody 來做移動。"), SerializeField]
        public float MoveSpeed { get; private set; }
    }
}
