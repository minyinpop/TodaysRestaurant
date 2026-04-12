using System;
using System.Collections.Generic;
using Common.Item.Data;
using Common.Value;
using UI_System.Message_UI_System.Child.Audio_UI_System;
using UI_System.Message_UI_System.Child.Defeat_UI_System.System;
using UI_System.Message_UI_System.Child.Dialogue_Skip_UI_System.System;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.System;
using UI_System.Message_UI_System.Child.Switch_UI_System.System;
using UI_System.Message_UI_System.Child.Tip_UI_System.System;
using UnityEngine;

namespace UI_System.Message_UI_System.Main
{
    public sealed class MessageUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private TipUISystem tipUISystem;
                                private static TipUISystem _tipUISystem;
        [field: SerializeField] private SwitchUISystem switchUISystem;
                                private static SwitchUISystem _switchUISystem;
        [field: SerializeField] private ItemGetUISystem itemGetUISystem;
                                private static ItemGetUISystem _itemGetUISystem;
        [field: SerializeField] private DefeatUISystem defeatUISystem;
                                private static DefeatUISystem _defeatUISystem;
        [field: SerializeField] private DialogueSkipUISystem dialogueSkipUISystem;
                                private static DialogueSkipUISystem _dialogueSkipUISystem;
        [field: SerializeField] private AudioUISystem audioUISystem;
                                private static AudioUISystem _audioUISystem;

        private void Awake()
        {
            if (tipUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(tipUISystem)} 沒有被掛載。");
            }
            
            if (switchUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(switchUISystem)} 沒有被掛載。");
            }
            
            if (itemGetUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(itemGetUISystem)} 沒有被掛載。");
            }
            
            if (defeatUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(defeatUISystem)} 沒有被掛載。");
            }
            
            if (dialogueSkipUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(dialogueSkipUISystem)} 沒有被掛載。");
            }
            
            if (audioUISystem is null)
            {
                throw new InvalidOperationException($"{nameof(audioUISystem)} 沒有被掛載。");
            }

            _tipUISystem = tipUISystem;
            _switchUISystem = switchUISystem;
            _itemGetUISystem = itemGetUISystem;
            _defeatUISystem = defeatUISystem;
            _dialogueSkipUISystem = dialogueSkipUISystem;
            _audioUISystem = audioUISystem;
        }
        
        public static void ShowTipUI(PopUpUIContent content, Action onConfirm = null)
        {
            _tipUISystem.ShowUI(content, onConfirm);
        }
        
        public static void ShowSwitchUI(PopUpUIContent content, Action onConfirm, Action onCancel = null)
        {
            _switchUISystem.ShowUI(content, onConfirm, onCancel);
        }
        
        public static void ShowItemGetUI(PopUpUIContent content, IReadOnlyList<ItemSO> items, Action onConfirm)
        {
            _itemGetUISystem.ShowUI(content, items, onConfirm);
        }

        public static void ShowDefeatUI(PopUpUIContent content, Action onConfirm)
        {
            _defeatUISystem.ShowUI(content, onConfirm);
        }

        public static void ShowDialogueSkipUI(PopUpUIContent content, Action onConfirm, Action onCancel = null)
        {
            _dialogueSkipUISystem.ShowUI(content, onConfirm, onCancel);
        }

        public static void ShowAudioUI(string BGMName, Action onComplete = null)
        {
            _audioUISystem.StartShowName(BGMName);
        }
    }
}