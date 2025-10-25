using System.Collections.Generic;
using System.Restaurant.Child.Customer.Child;
using UnityEngine;

namespace System.Restaurant.Child.Customer.Main
{
    internal sealed class CustomerSystem : MonoBehaviour
    {
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private FlipSystem FlipSystem;
        [field: SerializeField] private SkinSystem SkinSystem;

        private readonly List<Action> ActiveActions = new();
        
        private void OnEnable()
        {
            MoveSystem.ToLeft += FlipSystem.TurnsLeft;
            ActiveActions.Add(() => MoveSystem.ToLeft -= FlipSystem.TurnsLeft);
                
            MoveSystem.ToRight += FlipSystem.TurnsRight;
            ActiveActions.Add(() => MoveSystem.ToRight -= FlipSystem.TurnsRight);
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }
        
        #region System
            #region MoveSystem
                public void WalkTo(Transform target, Action onArrive) => MoveSystem.StartWalk(target, onArrive);
            #endregion
            
            #region AnimationSystem
                public void PlayIdleAnima() => AnimationSystem.Idle();
                public void PlayWalkAnima() => AnimationSystem.Walk();
            #endregion
            
            #region SkinSystem
                public void SetSkin() => SkinSystem.SetRandomSkin();
            #endregion
        #endregion
    }
}