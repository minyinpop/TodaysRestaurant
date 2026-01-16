using System.Collections;
using Input_System.Main;
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
                        InputSystem.Disable();
                        UISystem.SpawnFoodMenu(() =>
                        {
                            UISystem.DestroyFoodMenu();
                            RoundStart();
                        });
                    }
                    
                    void OnExit()
                    {
                        InputSystem.Enable();
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