using System.General;
using Data.Animation.DOTween.Basic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace System.Battle.System.Child.Initiative_System.Object.Toss_Result_Text
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class TossResultText : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        [field: SerializeField] private TextMeshProUGUI Text;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation AnimationSystem;

        public void Show(Color color, string content, Action onComplete = null)
        {
            Text.color = color;
            Text.text = content;
            AnimationSystem.DoScale_UI(Rect, new DoScale(Vector2.one, .5f, Ease.OutBack),
                onComplete: () => onComplete?.Invoke());
        }

        public void Hide(float hideDuration)
        {
            AnimationSystem.DoScale_UI(Rect, new DoScale(Vector2.zero, hideDuration, Ease.InBack));
        }
    }
}