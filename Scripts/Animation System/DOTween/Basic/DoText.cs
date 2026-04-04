using DG.Tweening;
using UnityEngine;

namespace Animation_System.DOTween.Basic
{
    [System.Serializable]
    public sealed class DoText
    {
        [field: Header("Values")]
        [field: SerializeField, TextArea] private string endValue;
                                public string EndValue => endValue;
        
        [field: SerializeField] private float duration;
                                public float Duration => duration;
        
        [field: SerializeField] private bool richTextEnabled;
                                public bool RichTextEnabled => richTextEnabled;
        
        [field: SerializeField] private ScrambleMode scrambleMode;
                                public ScrambleMode ScrambleMode => scrambleMode;
        
        [field: SerializeField] private string scrambleChars;
                                public string ScrambleChars => scrambleChars;
                                
        [field: Header("Ease")]
        [field: SerializeField] private Ease ease;
                                public Ease Ease => ease;

        public DoText(string endValue, float duration, bool richTextEnabled, ScrambleMode scrambleMode, string scrambleChars)
        {
            this.endValue = endValue;
            this.duration = duration;
            this.richTextEnabled = richTextEnabled;
            this.scrambleMode = scrambleMode;
            this.scrambleChars = scrambleChars;
        }
    }
}