using System.General;
using Data.Animation.DOTween.Basic;
using Spine.Unity;
using UnityEngine;
using SpineAnimation = Data.Animation.Spine.SpineAnimation;

namespace System.Player.Character.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class AnimationSystem : MonoBehaviour
    {
        [field: Header("Renderer")]
        [field: SerializeField] private GameObject Renderer;
        [field: SerializeField] private SkeletonAnimation SkeletonAnimation;
        
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;

        [field: Header("Animation Settings")]
        [field: SerializeField] private SpineAnimation Idle_AnimationSettings;
        [field: SerializeField] private SpineAnimation Walk_AnimationSettings;
        [field: SerializeField] private DoRotate TurnLeft_AnimationSettings;
        [field: SerializeField] private DoRotate TurnRight_AnimationSettings;

        private void SetAnimation(SpineAnimation anima)
        {
            anima.GetValues(out var layer, out var animaName, out var loop);
            SkeletonAnimation.AnimationState.SetAnimation(layer, animaName, loop);
        }
        
        public void Idle() => SetAnimation(Idle_AnimationSettings);
        public void Walk() => SetAnimation(Walk_AnimationSettings);

        public void TurnsLeft() => DoAnimation.DoRotate(Renderer.transform, TurnLeft_AnimationSettings);
        public void TurnsRight() => DoAnimation.DoRotate(Renderer.transform, TurnRight_AnimationSettings);
    }
}