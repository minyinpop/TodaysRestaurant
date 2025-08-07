using UnityEngine;

namespace BATTLE.SYSTEM.INITIATIVE.DATA
{
    [System.Serializable]
    internal class InitiativeResultContent
    {
        [field: TextArea, SerializeField] public string Upper { get; private set; }
        [field: TextArea, SerializeField] public string Bottom { get; private set; }
    }
}