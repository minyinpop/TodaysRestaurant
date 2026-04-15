using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Spine;
using Spine.Unity;
using UnityEngine;
using SpineAnimation = Animation_System.Spine.SpineAnimation;

namespace Player_System.Object
{
    [RequireComponent(typeof(DoAnimation))]
    public partial class PlayerObject
    {
        [field: Header("動畫系統 - 組件")]
        [field: SerializeField] private GameObject rendererRoot;
        [field: SerializeField] private SkeletonAnimation skeletonAnimation;
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("動畫系統 - 動畫")]
        [field: SerializeField] private SpineAnimation idle;
        [field: SerializeField] private SpineAnimation walk;
        [field: SerializeField] private SpineAnimation take;
        [field: SerializeField] private DoRotate turnLeft;
        [field: SerializeField] private DoRotate turnRight;

        private TrackEntry SetAnimation(SpineAnimation anima)
        {
            anima.GetValues(out var layer, out var animaName, out var loop);
            var entry = skeletonAnimation.AnimationState.SetAnimation(layer, animaName, loop);
            return entry;
        }

        private void PlayIdleAnimation()
        {
            SetAnimation(idle);
        }
        
        private void PlayWalkAnimation()
        {
            var entry = SetAnimation(walk);
            
            entry.Event += (_, @event) =>
            {
                switch (@event.Data.Name)
                {
                    case "Footstep":
                    {
                        PlayWalkOnDirtSFX();
                        break;
                    }
                    default:
                    {
                        Debug.Log($"在 {nameof(PlayWalkAnimation)} 裡發現未被登入的事件名稱：{@event.Data.Name}");
                        break;
                    }
                }
            };
        }

        private void PlayTakeAnimation()
        {
            var entry = SetAnimation(take);

            entry.Event += (_, @event) =>
            {
                switch (@event.Data.Name)
                {
                    case "Take":
                    {
                        RemoveInteractableObject();
                        break;
                    }
                    default:
                    {
                        Debug.Log($"在 {nameof(PlayTakeAnimation)} 裡發現未被登入的事件名稱：{@event.Data.Name}");
                        break;
                    }
                }
            };

            entry.Complete += _ =>
            {
                _stateMachine.ChangeState(_idleState);
            };
        }

        private void TurnsLeft()
        {
            animation.DoRotate(rendererRoot.transform, turnLeft);
        }
        
        private void TurnsRight()
        {
            animation.DoRotate(rendererRoot.transform, turnRight);
        }
    }
}