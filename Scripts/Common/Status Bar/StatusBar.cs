using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Status_Bar
{
    internal sealed class StatusBar : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private Slider innerFill;
        [field: SerializeField] private Slider outerFill;

        private float _value;
        private float _maxValue = 1;
        
        private Tween _tween;

        public void Initialize(float value, float maxValue)
        {
            if (value > maxValue)
            {
                throw new ArgumentOutOfRangeException($"{name} > {GetType().Name} > {nameof(Initialize)} > {nameof(value)} cannot be greater than {nameof(maxValue)}.");
            }

            _value = value;
            _maxValue = maxValue;
            
            InitValue(innerFill);
            InitValue(outerFill);
            return;
            
            void InitValue(Slider slider)
            {
                slider.maxValue = _maxValue;
                slider.value = _value;
            }
        }
        
        public void Add(float value)
        {
            // TODO
        }
        
        public void Subtract(float value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"{name} > {GetType().Name} > {nameof(Subtract)} > {nameof(value)} cannot be less than 0.");
            }

            _value = Mathf.Clamp(_value -= value, 0, _maxValue);
            
            _tween?.Kill();
            _tween = DOTween.Sequence()
                .Append(outerFill
                    .DOValue(_value, .25f))
                .Join(innerFill
                    .DOValue(_value, 1))
                .OnKill(() => _tween = null);
        }
    }
}