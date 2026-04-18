using System;

namespace Common.Enemy_Explore_Object.State_Machine.State
{
    public sealed class OnIdle : IState
    {
        private readonly Action _onEnter;
        private readonly Action _onUpdate;
        private readonly Action _onExit;
        
        public OnIdle(Action onEnter, Action onUpdate, Action onExit)
        {
            _onEnter = onEnter;
            _onUpdate = onUpdate;
            _onExit = onExit;
        }

        public void Enter() => _onEnter.Invoke();
        public void Update() => _onUpdate.Invoke();
        public void Exit() => _onExit.Invoke();
    }
}