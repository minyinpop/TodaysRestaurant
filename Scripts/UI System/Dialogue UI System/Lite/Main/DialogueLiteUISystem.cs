using System;
using System.Collections;
using Common.Button;
using Common.Dialogue.Data;
using Common.Dialogue.SO.Child.Text.Show_Lite_Text;
using Common.Dialogue.SO.Main;
using UI_System.Dialogue_UI_System.Lite.Child;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Lite.Main
{
    public sealed class DialogueLiteUISystem : MonoBehaviour
    {
        [field: Header("系統")]
        [field: SerializeField] private DialogueUITextSystem textSystem;
        
        [field: Header("按鈕")]
        [field: SerializeField] private Button continueButton;
        
        private DialogueSO _dialogueData;
        
        private IEnumerator _dialogueCoroutine;

        private bool _continueDialogue;

        private void Awake()
        {
            if (textSystem is null)
            {
                throw new InvalidOperationException($"{nameof(textSystem)} 沒有被掛載。");
            }
            
            if (continueButton is null)
            {
                throw new InvalidOperationException($"{nameof(continueButton)} 沒有被掛載。");
            }
            
            continueButton.gameObject.SetActive(true);
            continueButton.SetInteractable(false);

            continueButton.OnClick += OnClickContinueButton;
        }
        
        private void OnDisable()
        {
            if (_dialogueCoroutine is not null)
            {
                StopCoroutine(_dialogueCoroutine);
                _dialogueCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            continueButton.OnClick -= OnClickContinueButton;
        }

        public void StartDialogue(DialogueSO dialogueData, Action onComplete)
        {
            if (dialogueData is null)
            {
                throw new ArgumentNullException(nameof(dialogueData), $"{nameof(dialogueData)} 不能傳入空值。");
            }
            
            if (_dialogueCoroutine is not null)
            {
                Debug.Log("已經有對話正在播放了！");
                return;
            }
            
            _dialogueData = dialogueData;
            
            _dialogueCoroutine = DialogueCoroutine(onComplete);
            StartCoroutine(_dialogueCoroutine);
        }

        private IEnumerator DialogueCoroutine(Action onComplete)
        {
            for (var i = _dialogueData.StartIndex - 1; i < _dialogueData.DialogueDataEntries.Length; i++)
            {
                Debug.Log($"= = = 段落 {i + 1:D2} = = =");
                
                var dialogueDataEntry = _dialogueData.DialogueDataEntries[i];

                foreach (var dialogueData in dialogueDataEntry.DialogueData)
                {
                    if (dialogueDataEntry.DialogueData is null)
                    {
                        continue;
                    }

                    Debug.Log($"現在播放：{dialogueData}");

                    switch (dialogueData.DialogueDataType)
                    {
                        case DialogueDataType.Show_Lite_Text:
                        {
                            if (!dialogueData.BlockProcess)
                            {
                                _continueDialogue = true;
                            }
                            
                            textSystem.ShowText(
                                dialogueData: dialogueData as ShowText,
                                onComplete: () =>
                                {
                                    if (!dialogueData.BlockProcess)
                                    {
                                        return;
                                    }
                                    
                                    if (!dialogueData.ClickToPass)
                                    {
                                        _continueDialogue = true;
                                    }
                                    
                                    continueButton.SetInteractable(true);
                                });
                            
                            break;
                        }
                        case DialogueDataType.Hide_Text:
                        {
                            textSystem.HideText(onComplete: () =>
                            {
                                _continueDialogue = true;
                            });
                            
                            break;
                        }
                        default:
                        {
                            Debug.Log($"檢測到未在 {nameof(DialogueLiteUISystem)} 裡登記的指令，將自動跳過。");
                            _continueDialogue = true;
                            break;
                        }
                    }
                    
                    yield return new WaitUntil(() => _continueDialogue);
                    
                    _continueDialogue = false;
                }
            }

            onComplete.Invoke();

            yield return null;
        }
        
        private void OnClickContinueButton()
        {
            continueButton.SetInteractable(false);
            _continueDialogue = true;
        }
    }
}