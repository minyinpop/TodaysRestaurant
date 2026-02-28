using UnityEngine;

namespace Common.Enemy.Enemy_Object.State_Machine
{
    public sealed class StateMachine
    {
        private bool _initialized;
        
        private IState _currentState;

        public void InitializeState(IState state)
        {
            if (state == null)
            {
                Debug.Log($"{nameof(InitializeState)} > {nameof(state)} cannot be null.");
                return;
            }

            if (_initialized)
            {
                Debug.Log($"{nameof(StateMachine)} is already initialized.");
                return;
            }

            _initialized = true;
            
            _currentState = state;
            _currentState.Enter();
        }

        public void ChangeState(IState state)
        {
            if (state == null)
            {
                Debug.Log($"{nameof(ChangeState)} > {nameof(state)} cannot be null.");
                return;
            }

            if (!_initialized)
            {
                Debug.Log($"{nameof(StateMachine)} need to be initialized first.");
                return;
            }

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void UpdateState()
        {
            if (_currentState == null)
            {
                Debug.Log($"{nameof(UpdateState)} > {nameof(_currentState)} cannot be null.");
                return;
            }
            
            if (!_initialized)
            {
                Debug.Log($"{nameof(StateMachine)} need to be initialized first.");
                return;
            }

            _currentState.Update();
        }
    }
}