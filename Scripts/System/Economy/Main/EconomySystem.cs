using System.Collections;
using System.Collections.Generic;
using System.Economy.Child.Food_Menu.System.Main;
using System.Economy.Child.Restaurant.System;
using System.Economy.Main.State_Machine;
using System.Economy.Main.State_Machine.State;
using UnityEngine;

namespace System.Economy.Main
{
    internal sealed class EconomySystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private FoodMenuSystem FoodMenuSystem;
        [field: SerializeField] private RestaurantSystem RestaurantSystem;
        
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
                    StateMachine.ChangeState(new PlayerChooseFoodOnCookMenu(OnEnter, OnExit));
                    return;
                    
                    void OnEnter()
                    {
                        FoodMenuSystem.Show();
                        FoodMenuSystem.OnClickOpenUIConfirmButton += RoundStart;
                        ActiveActions.Enqueue(() => FoodMenuSystem.OnClickOpenUIConfirmButton -= RoundStart);
                    }
                    
                    void OnExit()
                    {
                        FoodMenuSystem.OnClickOpenUIConfirmButton -= RoundStart;
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
                        RestaurantSystem.StartSystem();
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion
        #endregion
    }
}