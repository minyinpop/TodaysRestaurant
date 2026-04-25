using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.Object.State_Machine
{
    public sealed class StateMachine
    {
        private IState _currentState;

        private bool _initialized;

        public void InitializeState(IState newState)
        {
            if (_initialized)
            {
                Debug.Log("");
            }
        }

        public void ChangeState(IState newState)
        {
        }
    }
}