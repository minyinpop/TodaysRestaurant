using UnityEngine;

namespace Restaurant_System.Object.Cookware.System.State_Machine
{
    internal sealed class StateMachine
    {
        private bool _initialized;
        
        private IState _currentState;

        public void InitializeState(IState state)
        {
            if (state is null)
            {
                Debug.Log($"傳入的 {nameof(IState)} 不能是 null。");
                return;
            }

            if (_initialized)
            {
                Debug.Log($"{nameof(StateMachine)} 已經初始化過了。");
                return;
            }

            _initialized = true;
            
            _currentState = state;
            _currentState.Enter();
        }

        public void ChangeState(IState state)
        {
            if (state is null)
            {
                Debug.Log($"傳入的 {nameof(IState)} 不能是 null。");
                return;
            }

            if (!_initialized)
            {
                Debug.Log($"請先將 {nameof(StateMachine)} 初始化。");
                return;
            }

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void InteractState()
        {
            if (!_initialized)
            {
                Debug.Log($"請先將 {nameof(StateMachine)} 初始化。");
                return;
            }
            
            _currentState?.Interact();
        }
    }
}