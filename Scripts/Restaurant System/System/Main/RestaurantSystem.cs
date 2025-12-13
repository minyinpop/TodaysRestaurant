using System;
using System.Collections;
using System.Collections.Generic;
using Restaurant_System.Object.Food_Menu.System.Main;
using Restaurant_System.System.Child.Customer_Manager_System;
using Restaurant_System.System.Main.State_Machine;
using Restaurant_System.System.Main.State_Machine.State;
using UnityEngine;

namespace Restaurant_System.System.Main
{
    internal sealed class RestaurantSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private FoodMenu FoodMenu;
        
        [field: Header("System")]
        [field: SerializeField] private CustomerManagerSystem CustomerManagerSystem;
        
        private readonly Queue<Action> ActiveActions = new();

        private IEnumerator RoundStartCor;
        
        private void Start()
        {
            PlayerChooseFoodOnCookMenu();
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();

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
                        FoodMenu.Show();
                        FoodMenu.OnClickOpenUIConfirmButton += RoundStart;
                        ActiveActions.Enqueue(() => FoodMenu.OnClickOpenUIConfirmButton -= RoundStart);
                    }
                    
                    void OnExit()
                    {
                        FoodMenu.OnClickOpenUIConfirmButton -= RoundStart;
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