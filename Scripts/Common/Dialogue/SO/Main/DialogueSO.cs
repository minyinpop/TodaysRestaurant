using Common.Dialogue.Data;
using Common.Scene_Starter;
using UnityEngine;

namespace Common.Dialogue.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Main/Dialogue Data", fileName = "New Data")]
    public sealed class DialogueSO : SceneStarterData
    {
        [field: SerializeField] private DialogueType dialogueType;
                                public DialogueType DialogueType => dialogueType;

        [field: SerializeField] private string skipMessage;
                                public string SkipMessage => skipMessage;
        
        [field: SerializeField] private int startIndex;
                                public int StartIndex => startIndex;
        
        [field: SerializeField] private DialogueDataEntry[] dialogueDataEntries;
                                public DialogueDataEntry[] DialogueDataEntries => dialogueDataEntries;
    }

    [System.Serializable]
    public sealed class DialogueDataEntry
    {
        [field: SerializeField] private DialogueData[] dialogueData;
                                public DialogueData[] DialogueData => dialogueData;
    }
}