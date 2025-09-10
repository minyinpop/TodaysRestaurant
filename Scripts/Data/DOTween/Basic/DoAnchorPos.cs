using DG.Tweening;
using UnityEngine;

namespace Data.DOTween.Basic
{
    internal sealed class DoAnchorPos
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        private readonly bool Snapping;

        private readonly Ease Ease;
        
        /// <summary>
        /// 設定動畫的參數
        /// </summary>
        /// <param name="endValue">目標位置</param>
        /// <param name="duration">播放時長</param>
        /// <param name="snapping">是否對齊網格</param>
        /// <param name="ease">動畫曲線</param>
        public DoAnchorPos(Vector3 endValue, float duration, bool snapping, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            Snapping = snapping;
            
            Ease = ease;
        }

        /// <summary>
        /// 獲取所有的動畫參數
        /// </summary>
        /// <param name="endValue">回傳目標位置</param>
        /// <param name="duration">回傳播放時長</param>
        /// <param name="snapping">回傳是否對齊網格</param>
        /// <param name="ease">回傳動畫曲線</param>
        public void GetValues(out Vector3 endValue, out float duration, out bool snapping, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            snapping = Snapping;
            
            ease = Ease;
        }
    }
}