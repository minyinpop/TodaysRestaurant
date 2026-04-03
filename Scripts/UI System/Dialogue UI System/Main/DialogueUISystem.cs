using System;
using System.Collections;
using Common.Dialogue.Child.Background.Hide;
using Common.Dialogue.Child.Background.Show;
using Common.Dialogue.Child.Character.Animation;
using Common.Dialogue.Child.Character.Hide;
using Common.Dialogue.Child.Character.Show;
using Common.Dialogue.Data;
using Common.Dialogue.Main;
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
        
        private DialogueSO _dialogueData;
        
        private IEnumerator _dialogueCoroutine;

        public DialogueSO DevelopDialogue;

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
            #endregion
        }

        private void Start()
        {
            StartDialogue(DevelopDialogue);
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
            _dialogueData = null;
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
            var complete = false;
            
            foreach (var dialogueData in _dialogueData.DialogueData)
            {
                complete = false;
                
                switch (dialogueData.DialogueDataType)
                {
                    case DialogueDataType.Show_Background:
                    {
                        backgroundSystem.ShowBackground(
                            dialogueData: dialogueData as ShowBackground,
                            onComplete: () => complete = true);
                        break;
                    }
                    case DialogueDataType.Hide_Background:
                    {
                        backgroundSystem.HideBackground(
                            dialogueData: dialogueData as HideBackground,
                            onComplete: () => complete = true);
                        break;
                    }
                    case DialogueDataType.Show_Character:
                    {
                        characterSystem.ShowCharacter(
                            dialogueData: dialogueData as ShowCharacter,
                            onComplete: () => complete = true);
                        break;
                    }
                    case DialogueDataType.Hide_Character:
                    {
                        characterSystem.HideCharacter(
                            dialogueData: dialogueData as HideCharacter,
                            onComplete: () => complete = true);
                        break;
                    }
                    case DialogueDataType.Play_Character_Animation:
                    {
                        characterSystem.PlayCharacterAnimation(
                            dialogueData: dialogueData as PlayCharacterAnimation, 
                            onComplete: () => complete = true);
                        break;
                    }
                    case DialogueDataType.Show_Text:
                    {
                        break;
                    }
                    case DialogueDataType.Hide_Text:
                    {
                        break;
                    }
                    case DialogueDataType.Wait:
                    {
                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException(dialogueData.DialogueDataType.ToString());
                    }
                }
                
                yield return new WaitUntil(() => complete);
            }
        }
    }
}