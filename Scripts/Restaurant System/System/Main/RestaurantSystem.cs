using System.Collections;
using Input_System;
using Restaurant_System.System.Child;
using Restaurant_System.System.Main.State_Machine;
using Restaurant_System.System.Main.State_Machine.State;
using UI_System.Player_UI_System.Main;
using UI_System.Restaurant_UI_System.Main;
using UnityEngine;

namespace Restaurant_System.System.Main
{
    internal sealed class RestaurantSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CustomerManagerSystem CustomerManagerSystem;

        private IEnumerator RoundStartCor;
        
        private readonly StateMachine _stateMachine = new();

        private IState _chooseItemState;
        private IState _roundStartState;

        private void Awake()
        {
            _chooseItemState = new ChooseItem(
                onEnter: () =>
                {
                    InputSystem.Disable();
                    
                    PlayerUISystem.SetHotbarUI(false);
                    PlayerUISystem.SetBackpackUI(false);
                    
                    RestaurantUISystem.OpenFoodMenu(() =>
                    {
                        RestaurantUISystem.CloseFoodMenu();
                        _stateMachine.ChangeState(_roundStartState);
                    });
                },
                onExit: () =>
                {
                    InputSystem.Enable();
                    
                    PlayerUISystem.SetHotbarUI(true);
                    PlayerUISystem.SetBackpackUI(true);
                });
            
            _roundStartState = new RoundStart(
                onEnter: () =>
                {
                    CustomerManagerSystem.StartSystem();
                },
                onExit: () =>
                {
                });
        }

        private void Start()
        {
            _stateMachine.ChangeState(_chooseItemState);
        }

        private void OnDisable()
        {
            if (RoundStartCor is not null)
            {
                StopCoroutine(RoundStartCor);
                RoundStartCor = null;
            }
        }
    }
}