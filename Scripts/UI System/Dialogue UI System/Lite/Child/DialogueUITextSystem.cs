using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using Common.Dialogue.SO.Child.Text.Show_Lite_Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI_System.Dialogue_UI_System.Lite.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class DialogueUITextSystem : MonoBehaviour
    {
        [field: Header("動畫組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("對話面板")]
        [field: SerializeField] private GameObject dialoguePanel;
        [field: SerializeField] private DoScale scaleUpSettings;
        [field: SerializeField] private DoScale scaleDownSettings;
        
        [field: Header("對話顯示物件")]
        [field: SerializeField] private Image avatarImage;
        [field: SerializeField] private TextMeshProUGUI nameText;
        [field: SerializeField] private TextMeshProUGUI dialogueText;
        
        [field: Header("繼續指示")]
        [field: SerializeField] private Image continueIndicator;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
            
            if (dialoguePanel is null)
            {
                throw new InvalidOperationException($"{nameof(dialoguePanel)} 沒有被掛載。");
            }
            
            if (avatarImage is null)
            {
                throw new InvalidOperationException($"{nameof(avatarImage)} 沒有被掛載。");
            }
            
            if (nameText is null)
            {
                throw new InvalidOperationException($"{nameof(nameText)} 沒有被掛載。");
            }
            
            if (dialogueText is null)
            {
                throw new InvalidOperationException($"{nameof(dialogueText)} 沒有被掛載。");
            }
            
            if (continueIndicator is null)
            {
                throw new InvalidOperationException($"{nameof(continueIndicator)} 沒有被掛載。");
            }
        }

        public void ShowText(ShowText dialogueData, Action onComplete)
        {
            if (dialoguePanel.activeSelf)
            {
                animation.DoScale_UI(
                    rect: dialoguePanel.GetComponent<RectTransform>(),
                    settings: scaleUpSettings,
                    onComplete: () =>
                    {
                        ShowTextProcess();
                    });
            }
            else
            {
                ShowTextProcess();
            }

            return;

            void ShowTextProcess()
            {
                #region 重置對話框文字
                    nameText.text = string.Empty;
                    dialogueText.text = string.Empty;
                    
                    continueIndicator.gameObject.SetActive(false);
                #endregion
                
                #region 開啟對話框
                    dialoguePanel.SetActive(true);
                #endregion
                
                #region 設定角色名稱
                    switch (dialogueData.CharacterType)
                    {
                        case DialogueCharacterType.Narrator:
                        {
                            avatarImage.gameObject.SetActive(false);
                            avatarImage.sprite = null;
                            
                            nameText.gameObject.SetActive(false);
                            break;
                        }
                        case DialogueCharacterType.Bernard:
                        {
                            avatarImage.sprite = dialogueData.AvatarSprite;
                            avatarImage.gameObject.SetActive(true);

                            nameText.text = "伯";
                            nameText.gameObject.SetActive(true);
                            break;
                        }
                        case DialogueCharacterType.Ray:
                        {
                            avatarImage.sprite = dialogueData.AvatarSprite;
                            avatarImage.gameObject.SetActive(true);

                            nameText.text = "雷";
                            nameText.gameObject.SetActive(true);
                            break;
                        }
                        default:
                        {
                            Debug.Log($"{dialogueData.CharacterType} 未在 {nameof(ShowText)} 裡登記，將自動跳過。");
                            onComplete.Invoke();
                            return;
                        }
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
        }

        public void HideText(Action onComplete)
        {
            animation.DoScale_UI(
                rect: dialoguePanel.GetComponent<RectTransform>(),
                settings: scaleDownSettings,
                onComplete: () =>
                {
                    #region 關閉對話框
                        dialoguePanel.SetActive(false);
                    #endregion
            
                    #region 清除角色的名稱
                        nameText.text = string.Empty;
                    #endregion
            
                    #region 清除說的內容
                        dialogueText.text = string.Empty;
                    #endregion
            
                    #region 關閉繼續指示
                        continueIndicator.gameObject.SetActive(false);
                    #endregion
                    
                    onComplete.Invoke();
                });
        }
    }
}