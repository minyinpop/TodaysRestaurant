using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Database.Restaurant.Player.Attribute
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Player/Attribute", fileName = "New Data", order = 2)]
    public class PlayerAttributeSO : ScriptableObject
    {
        [field: Header("移動設定")]
        [field: SerializeField] public PlayerMoveAttribute MoveAttribute { get; set; }
        
        [field: Header("動畫設定")]
        [field: SerializeField] public PlayerAnimationAttribute AnimationAttribute { get; set; }
    }

    [Serializable]
    public class PlayerMoveAttribute
    {
        [field: SerializeField] public bool CanMove { get; set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
    }

    [Serializable]
    public class PlayerAnimationAttribute
    {
        [field: SerializeField] public Range Blink { get; set; }

        public float GetRandomBlinkTime() => Random.Range(Blink.Min, Blink.Max);
    }

    [Serializable]
    public class Range
    {
        [field: SerializeField] public float Max { get; set; }
        [field: SerializeField] public float Min { get; set; }
    }
}