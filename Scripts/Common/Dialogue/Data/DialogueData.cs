using UnityEngine;

namespace Common.Dialogue.Data
{
    public abstract class DialogueData : ScriptableObject
    {
        public abstract DialogueDataType DialogueDataType { get; }
        
        public abstract bool AutoPass { get; }
    }
}