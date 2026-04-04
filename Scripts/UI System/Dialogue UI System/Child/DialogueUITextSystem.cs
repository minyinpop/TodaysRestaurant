using System;
using Animation_System.DOTween;
using Common.Dialogue.Child.Text;
using Common.Dialogue.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_System.Dialogue_UI_System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class DialogueUITextSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Panel")]
        [field: SerializeField] private GameObject dialoguePanel;
        [field: SerializeField] private GameObject characterNamePanel;

        [field: Header("Text")]
        [field: SerializeField] private TextMeshProUGUI characterName;
        [field: SerializeField] private TextMeshProUGUI dialogueText;
        
        [field: Header("Indicator")]
        [field: SerializeField] private Image continueIndicator;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException(nameof(animation));
            }

            if (dialoguePanel is null)
            {
                throw new InvalidOperationException(nameof(dialoguePanel));
            }

            if (characterName is null)
            {
                throw new InvalidOperationException(nameof(characterName));
            }
            
            if (dialogueText is null)
            {
                throw new InvalidOperationException(nameof(dialogueText));
            }
            
            if (continueIndicator is null)
            {
                throw new InvalidOperationException(nameof(continueIndicator));
            }
            
            #region 初始化繼續指示
                continueIndicator.gameObject.SetActive(false);
            #endregion
        }

        public void ShowText(ShowText dialogueData, Action onComplete)
        {
            #region 重置對話框文字
                characterName.text = string.Empty;
                dialogueText.text = string.Empty;
                
                continueIndicator.gameObject.SetActive(false);
            #endregion
            
            #region 開啟對話框
                dialoguePanel.SetActive(true);
            #endregion
            
            #region 設定角色名稱
                if (dialogueData.CharacterType == DialogueCharacterType.Narrator)
                {
                    characterNamePanel.SetActive(false);
                    characterName.text = string.Empty;
                }
                else
                {
                    characterNamePanel.SetActive(true);
                    characterName.text = dialogueData.CharacterType.ToString();
                }
            #endregion
            
            #region 設定說的內容
                animation.DoText(
                    tmp: dialogueText,
                    settings: dialogueData.TextSettings,
                    onComplete: () =>
                    {
                        #region 顯示繼續指示
                            continueIndicator.gameObject.SetActive(true);
                        #endregion
                        
                        onComplete.Invoke();
                    });
            #endregion
        }

        public void HideText(HideText dialogueData, Action onComplete)
        {
            #region 關閉對話框
                dialoguePanel.gameObject.SetActive(false);
            #endregion
            
            #region 清除角色的名稱
                characterName.text = string.Empty;
            #endregion
            
            #region 清除說的內容
                dialogueText.text = string.Empty;
            #endregion
            
            #region 關閉繼續指示
                continueIndicator.gameObject.SetActive(false);
            #endregion

            onComplete.Invoke();
        }
    }
}