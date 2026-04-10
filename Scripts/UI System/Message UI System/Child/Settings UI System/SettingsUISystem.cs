using System;
using Animation_System.DOTween;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Settings_UI_System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class SettingsUISystem : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
        }
    }
}