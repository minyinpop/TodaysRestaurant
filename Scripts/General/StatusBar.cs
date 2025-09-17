using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    internal sealed class StatusBar : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private Slider InnerFill;
        [field: SerializeField] private Slider OuterFill;

        private float MaxValue = 1;
        private float MinValue = 0;
        
        private float CurrentValue;

        private Tween ValueTween;

        public void Init(float minValue, float maxValue)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            InitValue(InnerFill);
            InitValue(OuterFill);
            CurrentValue = MaxValue;
            return;
            
            void InitValue(Slider slider)
            {
                slider.maxValue = MaxValue;
                slider.minValue = MinValue;
                slider.value = MaxValue;
            }
        }
        
        public void Add(float value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue += value, MinValue, MaxValue);
            InnerFill.value = CurrentValue;
            OuterFill.value = CurrentValue;
        }
        
        public void Subtract(float value)
        {
            CurrentValue = Mathf.Clamp(CurrentValue -= value, MinValue, MaxValue);
            ValueTween?.Kill();
            ValueTween = OuterFill
                .DOValue(CurrentValue, .25f)
                .OnKill(() => ValueTween = null);
        }
    }
}