using TMPro;
using UnityEngine;

namespace BATTLE.SYSTEM.SELECTION
{
    [System.Serializable]
    internal class TextSettings
    {
        [field: Header("Content")]
        [field: SerializeField] public ContentSettings Heads { get; private set; }
        [field: SerializeField] public ContentSettings Tails { get; private set; }
    }
    
    [System.Serializable]
    internal class ContentSettings
    {
        [field: Header("Component")]
        [field: SerializeField] public TextMeshProUGUI TopTMP { get; private set; }
        [field: SerializeField] public TextMeshProUGUI BottomTMP { get; private set; }
        
        [field: Header("Content")]
        [field: SerializeField] public string TopContent { get; private set; }
        [field: SerializeField] public string BottomContent { get; private set; }
    }
}