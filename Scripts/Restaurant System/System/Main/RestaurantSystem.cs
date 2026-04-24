using System;
using System.Collections;
using Common.Scene_Starter;
using Input_System;
using Restaurant_System.Object.Cookware.System;
using Restaurant_System.System.Child;
using Restaurant_System.System.Main.State_Machine;
using Restaurant_System.System.Main.State_Machine.State;
using UI_System.Player_UI_System.Main;
using UI_System.Restaurant_UI_System.Main;
using UnityEngine;

namespace Restaurant_System.System.Main
{
    internal sealed class RestaurantSystem : SceneStarter
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool autoStart;
        
        [field: Header("系統")]
        [field: SerializeField] private CustomerManagerSystem customerManagerSystem;
        
        [field: Header("廚具")]
        [field: SerializeField] private CookwareSystem[] cookwares;
        
        private IEnumerator _roundStartCoroutine;
        
        private readonly StateMachine _stateMachine = new();

        private IState _chooseItemState;
        private IState _roundStartState;

        private void Awake()
        {
            _chooseItemState = new ChooseItem(
                onEnter: () =>
                {
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
                    InputSystem.EnablePlayerWalk();
                    
                    PlayerUISystem.SetHotbarUI(true);
                    PlayerUISystem.SetBackpackUI(true);
                });
            
            _roundStartState = new RoundStart(
                onEnter: () =>
                {
                    customerManagerSystem.StartSystem();
                    
                    foreach (var cookware in cookwares)
                    {
                        cookware.StartSystem();
                    }
                },
                onExit: () =>
                {
                });
        }
        
        private void OnDisable()
        {
            if (_roundStartCoroutine is not null)
            {
                StopCoroutine(_roundStartCoroutine);
                _roundStartCoroutine = null;
            }
        }

        public override void InvokeOnSceneLoad(Action onComplete)
        {
            if (autoStart)
            {
                StartSystem();
            }
            else
            {
                Debug.Log($"{nameof(RestaurantSystem)} 的 {nameof(autoStart)} 為 false，須從外部觸發 {nameof(StartSystem)}。");
            }
        }
        
        public void StartSystem()
        {
            _stateMachine.ChangeState(_chooseItemState);
        }
    }
}