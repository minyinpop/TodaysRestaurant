using Data.DOTween.Basic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace System.Battle_System.System.Child.Initiative_System.Object.Toss_Result_Text
{
    [RequireComponent(typeof(AnimationSystem))]
    internal sealed class TossResultText : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private TextMeshProUGUI Text;
        
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;

        public void Show(Color color, string content, Action onComplete = null)
        {
            Text.color = color;
            Text.text = content;
            AnimationSystem.ScaleTo(new DoScale(Vector2.one, 1, Ease.OutBack))
                .OnComplete(() => onComplete?.Invoke());
        }

        public void Hide(float hideDuration)
        {
            AnimationSystem.ScaleTo(new DoScale(Vector2.zero, hideDuration, Ease.InBack));
        }
    }
}