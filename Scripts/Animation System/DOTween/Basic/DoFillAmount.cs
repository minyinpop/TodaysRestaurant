using System;
using DG.Tweening;
using UnityEngine;

namespace Animation_System.DOTween.Basic
{
    [Serializable]
    public sealed class DoFillAmount
    {
        [field: Header("參數")]
        [field: SerializeField, Range(0, 1)] private float endValue;
                                             public float EndValue => endValue;
        [field: SerializeField] private float duration;
                                public float Duration => duration;
        
        [field: Header("動畫曲線")]
        [field: SerializeField] private Ease ease;
                                public Ease Ease => ease;

        public DoFillAmount(float endValue, float duration, Ease ease)
        {
            this.endValue = endValue;
            this.duration = duration;
            
            this.ease = ease;
        }
    }
}