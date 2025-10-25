using System.General;
using Data.Animation.DOTween.Basic;
using UnityEngine;

namespace System.Economy.Child.Customer.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class FlipSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private GameObject Renderer;
        
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;

        [field: Header("Animation Settings")]
        [field: SerializeField] private DoRotate TurnLeft_AnimationSettings;
        [field: SerializeField] private DoRotate TurnRight_AnimationSettings;

        public void TurnsLeft() => DoAnimation.DoRotate(Renderer.transform, TurnLeft_AnimationSettings);
        public void TurnsRight() => DoAnimation.DoRotate(Renderer.transform, TurnRight_AnimationSettings);
    }
}