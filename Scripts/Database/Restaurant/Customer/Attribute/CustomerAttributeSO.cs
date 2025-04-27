using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Database.Restaurant.Customer.Attribute
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Customer/Attribute", fileName = "New Data", order = 1)]
    public class CustomerAttributeSO : ScriptableObject
    {
        [field: Header("移動設定")]
        [field: SerializeField] public CustomerMoveAttribute MoveAttribute { get; set; }
        
        [field: Header("動畫設定")]
        [field: SerializeField] public CustomerAnimationAttribute AnimationAttribute { get; set; }
    }

    [Serializable]
    public class CustomerMoveAttribute
    {
        [field: SerializeField] public bool CanMove { get; set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
    }
    
    [Serializable]
    public class CustomerAnimationAttribute
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