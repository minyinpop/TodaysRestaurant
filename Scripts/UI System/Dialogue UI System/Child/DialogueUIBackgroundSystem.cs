using System;
using Animation_System.DOTween;
using Common.Dialogue.SO.Child.Background;
using UnityEngine;
using UnityEngine.UI;

namespace UI_System.Dialogue_UI_System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class DialogueUIBackgroundSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Objects")]
        [field: SerializeField] private Image backgroundImage;
        
        private void Awake()
        {
            #region 必要條件檢查
                if (animation is null)
                {
                    throw new InvalidOperationException(nameof(animation));
                }
                
                if (backgroundImage is null)
                {
                    throw new InvalidOperationException(nameof(backgroundImage));
                }
            #endregion
        }
        
        public void ShowBackground(ShowBackground dialogueData, Action onComplete)
        {
            #region 設定顯示圖片
                backgroundImage.sprite = dialogueData.BackgroundImage;
            #endregion
            
            #region 顯示圖片動畫
                animation.DoColor_Image(
                    image: backgroundImage,
                    settings: dialogueData.FadeInSettings,
                    onComplete: onComplete.Invoke);
            #endregion
        }
        
        public void HideBackground(HideBackground dialogueData, Action onComplete)
        {
            #region 隱藏圖片動畫
                animation.DoColor_Image(
                    image: backgroundImage,
                    settings: dialogueData.FadeOutSettings,
                    onComplete: () =>
                    {
                        #region 清空圖片顯示
                            backgroundImage.sprite = null;
                        #endregion
                        
                        onComplete.Invoke();
                    });
            #endregion
        }
    }
}