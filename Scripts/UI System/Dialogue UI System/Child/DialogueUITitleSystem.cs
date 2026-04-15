using System;
using Animation_System.DOTween;
using Common.Dialogue.SO.Child.Title;
using TMPro;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class DialogueUITitleSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Object")]
        [field: SerializeField] private TextMeshProUGUI title;
        [field: SerializeField] private TextMeshProUGUI subtitle;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException(nameof(animation));
            }
            
            if (title is null)
            {
                throw new InvalidOperationException(nameof(title));
            }
            
            if (subtitle is null)
            {
                throw new InvalidOperationException(nameof(subtitle));
            }
            
            #region 初始化設定
                title.text = string.Empty;
                subtitle.text = string.Empty;
                
                title.gameObject.SetActive(false);
                subtitle.gameObject.SetActive(false);
            #endregion
        }

        public void ShowTitle(ShowTitle dialogueData, Action onComplete)
        {
            title.gameObject.SetActive(true);
            
            animation.DoText(
                tmp: title,
                settings: dialogueData.TitleSettings,
                onComplete: () =>
                {
                    subtitle.gameObject.SetActive(true);
                    
                    animation.DoText(
                        tmp: subtitle,
                        settings: dialogueData.SubtitleSettings,
                        onComplete: () =>
                        {
                            onComplete.Invoke();
                        });
                });
        }

        public void HideTitle(HideTitle dialogueData, Action onComplete)
        {
            #region 初始化設定
                title.text = string.Empty;
                subtitle.text = string.Empty;
                    
                title.gameObject.SetActive(false);
                subtitle.gameObject.SetActive(false);
            #endregion
            
            onComplete.Invoke();
        }
    }
}