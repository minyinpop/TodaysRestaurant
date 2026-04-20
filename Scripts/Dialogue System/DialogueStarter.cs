using System;
using Common.Dialogue.Data;
using Common.Dialogue.SO.Main;
using Common.Scene_Starter;
using UI_System.Dialogue_UI_System.Full.Main;
using UnityEngine;

namespace Dialogue_System
{
    public sealed class DialogueStarter : SceneStarter
    {
        [field: Header("系統")]
        [field: SerializeField] private DialogueFullUISystem dialogueFullUISystem;

        private void Awake()
        {
            if (dialogueFullUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(dialogueFullUISystem)} 沒有被掛載。");
            }
        }

        public override void InvokeOnSceneLoad(SceneStarterData starterData, Action onComplete)
        {
            if (starterData is DialogueSO dialogueData)
            {
                switch (dialogueData.DialogueType)
                {
                    case DialogueType.Full:
                    {
                        dialogueFullUISystem.StartDialogue(dialogueData);
                        onComplete.Invoke();
                        break;
                    }
                    default:
                    {
                        throw new InvalidOperationException($"{nameof(DialogueStarter)} 使用了未登記的 {nameof(DialogueSO)}。");
                    }
                }
            }
            else
            {
                throw new ArgumentException($"{nameof(starterData)} 必須是 {nameof(DialogueSO)}。");
            }
        }
    }
}