using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Status_Bar
{
    internal sealed class StatusBar : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private Slider InnerFill;
        [field: SerializeField] private Slider OuterFill;

        private float MaxValue = 1;
        
        private float CurrentValue;

        private Tween CurrentTween;

        public void Initialize(float maxValue)
        {
            MaxValue = maxValue;
            
            InitValue(InnerFill);
            InitValue(OuterFill);
            
            CurrentValue = MaxValue;
            return;
            
            void InitValue(Slider slider)
            {
                slider.maxValue = MaxValue;
                slider.value = MaxValue;
            }
        }
        
        public void Add(float value)
        {
            // TODO
        }
        
        public void Subtract(float value, Action isAlive, Action isDeath)
        {
            CurrentValue = Mathf.Clamp(CurrentValue -= value, 0, MaxValue);
            if (Mathf.Approximately(CurrentValue, 0))
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