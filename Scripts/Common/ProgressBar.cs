using System;
using Data.Animation.DOTween.Basic;
using Tool;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class ProgressBar : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Object")]
        [field: SerializeField] private Slider Slider;
        
        [field: Header("Value")]
        [field: SerializeField] private float MaxValue;
        [field: SerializeField] private float MinValue;
        [field: SerializeField] private float InitialValue;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoValue_Slider AnimationSettings;

        public event Action OnMaxValue;
        
        private bool IsMaxValueReached;

        private void Awake()
        {
            MaxValue = Mathf.Abs(MaxValue);
            MinValue = Mathf.Abs(MinValue);
            InitialValue = Mathf.Clamp(Mathf.Abs(InitialValue), MinValue, MaxValue);
            Slider.maxValue = MaxValue;
            Slider.minValue = MinValue;
            Slider.value = InitialValue;
        }

        public void Add(float value)
        {
            if (IsMaxValueReached) return;
            
            InitialValue = Mathf.Clamp(InitialValue + value, MinValue, MaxValue);
            AnimationSettings.GetValues(out _, out var duration, out var snapping, out var ease);
            
            var newAnimationSettings = new DoValue_Slider(InitialValue, duration, snapping, ease);
            DoAnimation.DoValue_Slider(Slider, newAnimationSettings);
            
            if (Mathf.Approximately(InitialValue, MaxValue)) OnMaxValue?.Invoke();
        }
    }
}