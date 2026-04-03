using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Main/Dialogue Data", fileName = "New Data")]
    public sealed class DialogueSO : ScriptableObject
    {
        [field: SerializeField] private DialogueData[] dialogueData;
                                public DialogueData[] DialogueData => dialogueData;
    }
}