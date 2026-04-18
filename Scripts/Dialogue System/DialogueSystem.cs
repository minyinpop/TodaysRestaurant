using System;
using Common.Dialogue.SO.Main;
using Common.Scene_Starter;
using UI_System.Dialogue_UI_System.Main;
using UnityEngine;

namespace Dialogue_System
{
    public sealed class DialogueSystem : SceneStarter
    {
        [field: Header("系統")]
        [field: SerializeField] private DialogueUISystem dialogueUISystem;

        private void Awake()
        {
            if (dialogueUISystem is null)
            {
                throw new InvalidOperationException(nameof(dialogueUISystem));
            }
        }

        public override void InvokeOnSceneLoad(SceneStarterData starterData, Action onComplete)
        {
            if (starterData is DialogueSO dialogueData)
            {
                dialogueUISystem.StartDialogue(dialogueData);
                onComplete.Invoke();
            }
            else
            {
                throw new ArgumentException($"{nameof(starterData)} must be {nameof(DialogueSO)}");
            }
        }
    }
}