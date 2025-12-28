using System.Collections;
using Restaurant_System.System.Child;
using Restaurant_System.System.Main.State_Machine;
using Restaurant_System.System.Main.State_Machine.State;
using UI_System.System.Main;
using UnityEngine;

namespace Restaurant_System.System.Main
{
    internal sealed class RestaurantSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CustomerManagerSystem CustomerManagerSystem;

        private IEnumerator RoundStartCor;
        
        private void Start()
        {
            PlayerChooseFoodOnCookMenu();
        }

        private void OnDisable()
        {
            if (RoundStartCor is not null)
            {
                StopCoroutine(RoundStartCor);
                RoundStartCor = null;
            }
        }

        #region State Machine
            #region PlayerChooseFoodOnCookMenu
                private readonly StateMachine StateMachine = new();
                private void PlayerChooseFoodOnCookMenu()
                {
                    StateMachine.ChangeState(new ChooseItem(OnEnter, OnExit));
                    return;
                    
                    void OnEnter()
                    {
                        UISystem.ShowFoodMenu(RoundStart);
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion

            #region RoundStart
                private void RoundStart()
                {
                    StateMachine.ChangeState(new RoundStart(OnEnter, OnExit));
                    return;
                    
                    void OnEnter()
                    {
                        CustomerManagerSystem.StartSystem();
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion
        #endregion
    }
}