using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace System
{
    internal sealed class StatusBar : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private Slider InnerFill;
        [field: SerializeField] private Slider OuterFill;

        private float MaxValue = 1;
        private float MinValue;
        
        private float CurrentValue;

        private Tween CurrentTween;

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
            // TODO
        }
        
        public void Subtract(float value, Action isAlive, Action isDeath)
        {
            CurrentValue = Mathf.Clamp(CurrentValue -= value, MinValue, MaxValue);
            if (Mathf.Approximately(CurrentValue, MinValue))
                isDeath?.Invoke();
            else
                isAlive?.Invoke();
            CurrentTween?.Kill();
            CurrentTween = DOTween.Sequence()
                .Append(OuterFill
                    .DOValue(CurrentValue, .25f))
                .Join(InnerFill
                    .DOValue(CurrentValue, 1))
                .OnKill(() => CurrentTween = null);
        }
    }
}