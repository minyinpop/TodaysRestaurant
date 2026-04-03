using Common.Dialogue.Main;
using UnityEngine;

namespace Common.Dialogue.Data
{
    public abstract class DialogueData : ScriptableObject
    {
        public abstract DialogueDataType DialogueDataType { get; }
    }
}