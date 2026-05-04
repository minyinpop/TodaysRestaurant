using System;
using UnityEngine;

namespace UI_System.Title_UI_System.Object
{
    public sealed class TitleMaskLogo : MonoBehaviour
    {
        [field: Header("動畫組件")]
        [field: SerializeField] private Animator animator;
        [field: SerializeField] private AnimationClip animationClip;

        public event Action OnComplete;

        public void Show()
        {
            animator.Play(Animator.StringToHash(animationClip.name));
        }

        // Animation Event 使用
        private void InvokeOnComplete()
        {
            if (OnComplete is null)
            {
                throw new InvalidOperationException($"{nameof(OnComplete)} 沒有被訂閱。");
            }

            OnComplete.Invoke();
        }
    }
}