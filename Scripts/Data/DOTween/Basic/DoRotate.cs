using DG.Tweening;
using UnityEngine;

namespace Data.DOTween.Basic
{
    internal sealed class DoRotate
    {
        private readonly Vector3 EndValue;
        private readonly float Duration;
        private readonly RotateMode RotateMode;
        
        private readonly Ease Ease;
        
        /// <summary>
        /// 設定動畫的參數
        /// </summary>
        /// <param name="endValue">目標位置</param>
        /// <param name="duration">播放時長</param>
        /// <param name="rotateMode">旋轉的模式</param>
        /// <param name="ease">動畫曲線</param>
        public DoRotate(Vector3 endValue, float duration, RotateMode rotateMode, Ease ease)
        {
            EndValue = endValue;
            Duration = duration;
            RotateMode = rotateMode;
            
            Ease = ease;
        }

        /// <summary>
        /// 獲取所有的動畫參數
        /// </summary>
        /// <param name="endValue">回傳目標位置</param>
        /// <param name="duration">回傳播放時長</param>
        /// <param name="rotateMode">旋轉的模式</param>
        /// <param name="ease">回傳動畫曲線</param>
        public void GetValues(out Vector3 endValue, out float duration, out RotateMode rotateMode, out Ease ease)
        {
            endValue = EndValue;
            duration = Duration;
            rotateMode = RotateMode;
            
            ease = Ease;
        }
    }
}