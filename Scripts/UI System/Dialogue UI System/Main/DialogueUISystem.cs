using System;
using System.Collections;
using Common.Button;
using Common.Database;
using Common.Dialogue.Data;
using Common.Dialogue.SO.Child.Background;
using Common.Dialogue.SO.Child.Character;
using Common.Dialogue.SO.Child.Sound;
using Common.Dialogue.SO.Child.Text;
using Common.Dialogue.SO.Child.Title;
using Common.Dialogue.SO.Child.Tool;
using Common.Dialogue.SO.Main;
using Common.Scene_Name;
using Common.Scene_Starter;
using Common.Value;
using UI_System.Dialogue_UI_System.Child;
using UI_System.Message_UI_System.Main;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Main
{
    public sealed class DialogueUISystem : MonoBehaviour
    {
        [field: Header("系統")]
        [field: SerializeField] private DialogueUIBackgroundSystem backgroundSystem;
        [field: SerializeField] private DialogueUICharacterSystem characterSystem;
        [field: SerializeField] private DialogueUITextSystem textSystem;
        [field: SerializeField] private DialogueUITitleSystem titleSystem;
        [field: SerializeField] private DialogueUISoundSystem soundSystem;
        
        [field: Header("按鈕")]
        [field: SerializeField] private Button continueButton;
        [field: SerializeField] private Button skipButton;
        
        private DialogueSO _dialogueData;
        
        private IEnumerator _dialogueCoroutine;

        private bool _continueDialogue;

        public static event Action<SceneNameSO, SceneStarterData> OnChangeScene;

        private void Awake()
        {
            #region 必要條件檢查
                if (backgroundSystem is null)
                {
                    throw new InvalidOperationException($"{nameof(backgroundSystem)} 沒有被掛載。");
                }
                
                if (characterSystem is null)
                {
                    throw new InvalidOperationException($"{nameof(characterSystem)} 沒有被掛載。");
                }
                
                if (textSystem is null)
                {
                    throw new InvalidOperationException($"{nameof(textSystem)} 沒有被掛載。");
                }

                if (titleSystem is null)
                {
                    throw new InvalidOperationException($"{nameof(titleSystem)} 沒有被掛載。");
                }
                
                if (soundSystem is null)
                {
                    throw new InvalidOperationException($"{nameof(soundSystem)} 沒有被掛載。");
                }

                if (continueButton is null)
                {
                    throw new InvalidOperationException($"{nameof(continueButton)} 沒有被掛載。");
                }

                if (skipButton is null)
                {
                    throw new InvalidOperationException($"{nameof(skipButton)} 沒有被掛載。");
                }
            #endregion
            
            #region 初始化按鈕狀態
                continueButton.gameObject.SetActive(true);
                continueButton.SetInteractable(false);
            #endregion

            continueButton.OnClick += OnClickContinueButton;
            skipButton.OnClick += OnClickSkipButton;
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
            skipButton.OnClick -= OnClickSkipButton;
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
                        case DialogueDataType.Fade_In_BGM:
                        {
                            soundSystem.FadeInBGM(
                                dialogueData: dialogueData as FadeInBGM,
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
                        case DialogueDataType.Fade_Out_BGM:
                        {
                            soundSystem.FadeOutBGM(
                                dialogueData: dialogueData as FadeOutBGM,
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
                        case DialogueDataType.Play_SFX:
                        {
                            soundSystem.PlaySFX(
                                dialogueData: dialogueData as PlaySFX,
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

        private void OnClickSkipButton()
        {
            MessageUISystem.ShowDialogueSkipUI(
                content: new PopUpUIContent(
                    message: _dialogueData.SkipMessage,
                    confirmButtonTitle: "跳過劇情",
                    cancelButtonTitle: "繼續觀看",
                    closeButtonTitle: string.Empty),
                onConfirm: () =>
                {
                    foreach (var dialogueDataEntry in _dialogueData.DialogueDataEntries)
                    {
                        foreach (var dialogueData in dialogueDataEntry.DialogueData)
                        {
                            if (dialogueData is not ChangeScene changeSceneData)
                            {
                                continue;
                            }

                            if (OnChangeScene is null)
                            {
                                throw new InvalidOperationException($"{nameof(OnChangeScene)} 沒有 class 訂閱。");
                            }
                            
                            SceneNameDatabase.GetSceneName(changeSceneData.SceneNameType, out var sceneNameData);
                            
                            OnChangeScene.Invoke(sceneNameData, changeSceneData.SceneStarterData);
                            return;
                        }
                    }
                });
        }
    }
}