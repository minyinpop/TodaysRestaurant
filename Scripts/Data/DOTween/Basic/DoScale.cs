using DG.Tweening;
using UnityEngine;

namespace Data.DOTween.Basic
{
    internal sealed class DoScale
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        
        private readonly Ease Ease;
        
        /// <summary>
        /// 設定動畫的參數
        /// </summary>
        /// <param name="endValue">目標位置</param>
        /// <param name="duration">播放時長</param>
        /// <param name="ease">動畫曲線</param>
        public DoScale(Vector3 endValue, float duration, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            
            Ease = ease;
        }
        
        /// <summary>
        /// 設定動畫的參數
        /// </summary>
        /// <param name="endValue">目標位置</param>
        /// <param name="duration">播放時長</param>
        /// <param name="ease">動畫曲線</param>
        public void GetValues(out Vector3 endValue, out float duration, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            
            ease = Ease;
        }
    }
}