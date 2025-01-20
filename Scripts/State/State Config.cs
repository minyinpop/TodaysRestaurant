using UnityEngine;

namespace State
{
    [CreateAssetMenu(menuName = "Minyinpop/New Config/State", fileName = "New State Config", order = 1)]
    public class StateConfig : ScriptableObject
    {
        [field: Tooltip("目標的移動速度"), SerializeField]
        public float MoveSpeed { get; private set; }
    }
}
