using DG.Tweening;
using UnityEngine;

namespace BATTLE.SYSTEM
{
    /// <summary>
    /// 戰鬥進程的子系統
    /// 讓螢幕出現遮罩的程式碼，用於加強顯示重點
    /// </summary>
    internal class ScreenMaskSystem : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [field: SerializeField] private CanvasGroup ScreenMask;

        /// <summary>
        /// 
        /// </summary>
        private Tween FadeTween;

        /// <summary>
        /// 先中斷上個 FadeTween，
        /// </summary>
        public void ShowMask()
        {
            FadeTween?.Kill();
            FadeTween = null;
            
            FadeTween = ScreenMask.DOFade(1, 1);
        }

        /// <summary>
        /// 中斷當前會把透明度 1 調整至 0
        /// </summary>
        public void HideMask()
        {
            FadeTween?.Kill();
            FadeTween = null;
            
            FadeTween = ScreenMask.DOFade(0, 1);
        }
    }
}