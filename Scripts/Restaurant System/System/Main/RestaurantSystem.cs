using System;
using System.Collections;
using Audio_System.Data;
using Audio_System.Main;
using Common.Scene_Starter;
using Input_System;
using Restaurant_System.Object.Cookware.System;
using Restaurant_System.System.Child;
using Restaurant_System.System.Main.State_Machine;
using Restaurant_System.System.Main.State_Machine.State;
using UI_System.Player_UI_System.Main;
using UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.System;
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
        [field: SerializeField] private OpenClosedUISystem openClosedUISystem;
        
        [field: Header("廚具")]
        [field: SerializeField] private CookwareSystem[] cookwares;
        
        [field: Header("音樂")]
        [field: SerializeField] private FadeInBGMData fadeInBGM;
        
        private IEnumerator _roundStartCoroutine;
        
        private readonly StateMachine _stateMachine = new();

        private IState _onFoodMenuState;
        private IState _onRestaurantOpenState;
        private IState _onRestaurantClosedState;

        private void Awake()
        {
            _onFoodMenuState = new OnFoodMenu(
                onEnter: () =>
                {
                    PlayerUISystem.SetHotbarUI(false);
                    
                    RestaurantUISystem.OpenFoodMenu(() =>
                    {
                        RestaurantUISystem.CloseFoodMenu();
                        
                        _stateMachine.ChangeState(_onRestaurantClosedState);
                    });
                },
                onExit: () =>
                {
                    InputSystem.EnablePlayerWalk();
                    
                    PlayerUISystem.SetHotbarUI(true);
                });

            _onRestaurantOpenState = new OnRestaurantOpen(
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
            
            _onRestaurantClosedState = new OnRestaurantClosed(
                onEnter: () =>
                {
                },
                onExit: () =>
                {
                });
            
            AudioSystem.Instance.CommonBGM.FadeInBGM(
                data:  fadeInBGM,
                onComplete: () =>
                {
                    Debug.Log($"{fadeInBGM.ChangeClip} 播放成功。");
                });

            openClosedUISystem.OnOpen += InvokeRestaurantOpen;
            openClosedUISystem.OnClosed += InvokeRestaurantClosed;
        }
        
        private void OnDisable()
        {
            openClosedUISystem.OnOpen -= InvokeRestaurantOpen;
            openClosedUISystem.OnClosed -= InvokeRestaurantClosed;
            
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
                InvokeFoodMenu();
            }
            else
            {
                Debug.Log($"{nameof(RestaurantSystem)} 的 {nameof(autoStart)} 為 false，須從外部觸發。");
            }
        }

        public void InvokeFoodMenu()
        {
            _stateMachine.ChangeState(_onFoodMenuState);
        }

        private void InvokeRestaurantOpen()
        {
            _stateMachine.ChangeState(_onRestaurantOpenState);
        }

        private void InvokeRestaurantClosed()
        {
            _stateMachine.ChangeState(_onRestaurantClosedState);
        }
    }
}