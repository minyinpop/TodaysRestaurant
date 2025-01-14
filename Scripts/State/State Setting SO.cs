using UnityEngine;

namespace State
{
    [CreateAssetMenu(menuName = "Minyinpop/State Config", fileName = "New State Config", order = 1)]
    public class StateConfigSO : ScriptableObject
    {
        [field: Header("人物")] [field: Tooltip("移動速度"), SerializeField]
        public float MoveSpeed { get; private set; }
    }
}
