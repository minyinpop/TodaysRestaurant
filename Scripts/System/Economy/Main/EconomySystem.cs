using System.Collections;
using System.Collections.Generic;
using System.Economy.Child.Cook_Menu.System.Main;
using System.Economy.Child.Restaurant.System;
using System.Economy.Main.State_Machine;
using System.Economy.Main.State_Machine.State;
using UnityEngine;

namespace System.Economy.Main
{
    internal sealed class EconomySystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CookMenuSystem CookMenuSystem;
        [field: SerializeField] private RestaurantSystem RestaurantSystem;
        
        private readonly List<Action> ActiveActions = new();

        private IEnumerator RoundStartCor;
        
        private void Start()
        {
            RoundStart();
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
                        CookMenuSystem.Show();
                        CookMenuSystem.OnClickOpenUIConfirmButton += RoundStart;
                        ActiveActions.Add(() => CookMenuSystem.OnClickOpenUIConfirmButton -= RoundStart);
                    }
                    
                    void OnExit()
                    {
                        CookMenuSystem.OnClickOpenUIConfirmButton -= RoundStart;
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