using System;
using System.Collections;
using Common.Button;
using Common.Database;
using Common.Dialogue.Child.Background;
using Common.Dialogue.Child.Character;
using Common.Dialogue.Child.Text;
using Common.Dialogue.Child.Title;
using Common.Dialogue.Child.Tool;
using Common.Dialogue.Data;
using Common.Dialogue.Main;
using Common.Scene_Name;
using Common.Scene_Starter;
using UI_System.Dialogue_UI_System.Child;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Main
{
    public sealed class DialogueUISystem : MonoBehaviour
    {
        [field: Header("Systems")]
        [field: SerializeField] private DialogueUIBackgroundSystem backgroundSystem;
        [field: SerializeField] private DialogueUICharacterSystem characterSystem;
        [field: SerializeField] private DialogueUITextSystem textSystem;
        [field: SerializeField] private DialogueUITitleSystem titleSystem;
        
        [field: Header("Button")]
        [field: SerializeField] private Button continueButton;
        
        private DialogueSO _dialogueData;
        
        private IEnumerator _dialogueCoroutine;

        private bool _continueDialogue;

        public static event Action<SceneNameSO, SceneStarterData> OnChangeScene;

        private void Awake()
        {
            #region 必要條件檢查
                if (backgroundSystem is null)
                {
                    throw new InvalidOperationException(nameof(backgroundSystem));
                }
                
                if (characterSystem is null)
                {
                    throw new InvalidOperationException(nameof(characterSystem));
                }
                
                if (textSystem is null)
                {
                    throw new InvalidOperationException(nameof(textSystem));
                }

                if (titleSystem is null)
                {
                    throw new InvalidOperationException(nameof(titleSystem));
                }

                if (continueButton is null)
                {
                    throw new InvalidOperationException(nameof(continueButton));
                }
            #endregion
            
            #region 初始化按鈕狀態
                continueButton.gameObject.SetActive(true);
                continueButton.SetInteractable(false);
            #endregion

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

        public void StartDialogue(DialogueSO dialogueData)
        {
            #region 必要條件檢查
                if (dialogueData is null)
                {
                    throw new InvalidOperationException(nameof(dialogueData));
                }

                if (_dialogueCoroutine is not null)
                {
                    Debug.Log("已經有對話正在播放了！");
                    return;
                }
            #endregion

            _dialogueData = dialogueData;
            
            _dialogueCoroutine = DialogueCoroutine();
            StartCoroutine(_dialogueCoroutine);
        }
        
        private IEnumerator DialogueCoroutine()
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
                        case DialogueDataType.Show_Background:
                        {
                            backgroundSystem.ShowBackground(
                                dialogueData: dialogueData as ShowBackground,
                                onComplete: () =>
                                {
                                    if (dialogueData.AutoPass)
                                    {
                                        _continueDialogue = true;
                                        return;
                                    }
                                    
                                    continueButton.SetInteractable(true);
                                });
                            break;
                        }
                        case DialogueDataType.Hide_Background:
                        {
                            backgroundSystem.HideBackground(
                                dialogueData: dialogueData as HideBackground,
                                onComplete: () =>
                                {
                                    if (dialogueData.AutoPass)
                                    {
                                        _continueDialogue = true;
                                        return;
                                    }
                                    
                                    continueButton.SetInteractable(true);
                                });
                            break;
                        }
                        case DialogueDataType.Show_Character:
                        {
                            characterSystem.ShowCharacter(
                                dialogueData: dialogueData as ShowCharacter,
                                onComplete: () => _continueDialogue = true);
                            break;
                        }
                        case DialogueDataType.Hide_Character:
                        {
                            characterSystem.HideCharacter(
                                dialogueData: dialogueData as HideCharacter,
                                onComplete: () => _continueDialogue = true);
                            break;
                        }
                        case DialogueDataType.Play_Character_Animation:
                        {
                            characterSystem.PlayCharacterAnimation(
                                dialogueData: dialogueData as PlayCharacterAnimation, 
                                onComplete: () => _continueDialogue = true);
                            break;
                        }
                        case DialogueDataType.Show_Text:
                        {
                            textSystem.ShowText(
                                dialogueData: dialogueData as ShowText,
                                onComplete: () =>
                                {
                                    if (dialogueData.AutoPass)
                                    {
                                        _continueDialogue = true;
                                        return;
                                    }
                                    
                                    continueButton.SetInteractable(true);
                                });
                            break;
                        }
                        case DialogueDataType.Hide_Text:
                        {
                            textSystem.HideText(
                                dialogueData: dialogueData as HideText,
                                onComplete: () =>
                                {
                                    if (dialogueData.AutoPass)
                                    {
                                        _continueDialogue = true;
                                        return;
                                    }
                                    
                                    continueButton.SetInteractable(true);
                                });
                            break;
                        }
                        case DialogueDataType.Show_Title:
                        {
                            titleSystem.ShowTitle(
                                dialogueData: dialogueData as ShowTitle,
                                onComplete: () =>
                                {
                                    if (dialogueData.AutoPass)
                                    {
                                        _continueDialogue = true;
                                        return;
                                    }
                                    
                                    continueButton.SetInteractable(true);
                                });
                            break;
                        }
                        case DialogueDataType.Hide_Title:
                        {
                            titleSystem.HideTitle(
                                dialogueData: dialogueData as HideTitle,
                                onComplete: () =>
                                {
                                    if (dialogueData.AutoPass)
                                    {
                                        _continueDialogue = true;
                                        return;
                                    }
                                    
                                    continueButton.SetInteractable(true);
                                });
                            break;
                        }
                        case DialogueDataType.Wait:
                        {
                            if (dialogueData is Wait data)
                            {
                                yield return new WaitForSeconds(data.WaitTime);
                            }
                            
                            if (dialogueData.AutoPass)
                            {
                                _continueDialogue = true;
                                break;
                            }
                            
                            continueButton.SetInteractable(true);
                            break;
                        }
                        case DialogueDataType.ChangeScene:
                        {
                            var changeSceneData = dialogueData as ChangeScene;
                            
                            SceneNameDatabase.GetSceneName(changeSceneData.SceneNameType, out var sceneNameData);
                            
                            OnChangeScene.Invoke(sceneNameData, changeSceneData.SceneStarterData);
                            break;
                        }
                        default:
                        {
                            throw new ArgumentOutOfRangeException(dialogueData.DialogueDataType.ToString());
                        }
                    }
                    
                    yield return new WaitUntil(() => _continueDialogue);
                    
                    _continueDialogue = false;
                }
            }
        }

        private void OnClickContinueButton()
        {
            continueButton.SetInteractable(false);
            _continueDialogue = true;
        }
    }
}