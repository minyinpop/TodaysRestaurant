using System;
using Audio_System.Data;
using Audio_System.Main;
using Common.Restaurant_Statistical_Report;
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
    public sealed class RestaurantSystem : SceneStarter
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool autoStart;
        
        [field: Header("系統")]
        [field: SerializeField] private CustomerManagerSystem customerManagerSystem;
        [field: SerializeField] private RestaurantUISystem restaurantUISystem;
        
        [field: Header("廚具")]
        [field: SerializeField] private CookwareSystem[] cookwares;
        
        [field: Header("音樂")]
        [field: SerializeField] private FadeInBGMData fadeInBGM;
        
        private readonly StateMachine _stateMachine = new();

        private IState _onFoodMenuState;
        private IState _onRestaurantOpenState;
        private IState _onRestaurantClosedState;

        private RestaurantStatisticalReport _reportData = new();

        private void Awake()
        {
            _onFoodMenuState = new RestaurantState(
                onEnter: () =>
                {
                    PlayerUISystem.SetHotbarUI(false);
                    
                    restaurantUISystem.FoodMenuUISystem.OpenUI(
                        onConfirm: () =>
                        {
                            restaurantUISystem.FoodMenuUISystem.CloseUI();
                        });
                },
                onExit: () =>
                {
                    PlayerUISystem.SetHotbarUI(true);
                    
                    InputSystem.EnablePlayerWalk();
                });
            
            _onRestaurantOpenState = new RestaurantState(
                onEnter: () =>
                {
                    InvokeCookwareStart();
                    
                    customerManagerSystem.StartSystem();
                },
                onExit: () =>
                {
                    customerManagerSystem.EndSystem();
                });

            _onRestaurantClosedState = new RestaurantState(
                onEnter: () =>
                {
                    restaurantUISystem.StatisticalReportUISystem.OpenStatisticalTableUI(_reportData);
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

            restaurantUISystem.OpenClosedUISystem.OnOpen += InvokeRestaurantOpen;
            restaurantUISystem.OpenClosedUISystem.OnClosed += InvokeRestaurantClosed;

            customerManagerSystem.CustomerSpawned += InvokeCustomerSpawned;
            customerManagerSystem.CustomerHappy += InvokeCustomerHappy;
            customerManagerSystem.CustomerAngry += InvokeCustomerAngry;
        }

        private void OnDestroy()
        {
            restaurantUISystem.OpenClosedUISystem.OnOpen -= InvokeRestaurantOpen;
            restaurantUISystem.OpenClosedUISystem.OnClosed -= InvokeRestaurantClosed;
            
            customerManagerSystem.CustomerSpawned -= InvokeCustomerSpawned;
            customerManagerSystem.CustomerHappy -= InvokeCustomerHappy;
            customerManagerSystem.CustomerAngry -= InvokeCustomerAngry;
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

        private void InvokeCookwareStart()
        {
            foreach (var cookware in cookwares)
            {
                cookware.StartSystem();
            }
        }

        private void InvokeRestaurantOpen()
        {
            _stateMachine.ChangeState(_onRestaurantOpenState);
        }

        private void InvokeRestaurantClosed()
        {
            _stateMachine.ChangeState(_onRestaurantClosedState);
        }

        private void InvokeCustomerSpawned()
        {
            _reportData.TotalCustomerCount += 1;
        }

        private void InvokeCustomerHappy()
        {
            _reportData.HappyCustomerCount += 1;
        }

        private void InvokeCustomerAngry()
        {
            _reportData.AngryCustomerCount += 1;
        }
    }
}