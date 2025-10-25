using System.Collections;
using System.Collections.Generic;
using System.Cook.Child.Cook_Menu.System.Main;
using System.Cook.Child.Customer.System;
using System.Cook.Child.Queue.System;
using System.Cook.Child.Seating.System;
using System.Cook.Main.State_Machine;
using System.Cook.Main.State_Machine.State;
using UnityEngine;

namespace System.Cook.Main
{
    internal sealed class CookSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CookMenuSystem CookMenuSystem;
        [field: SerializeField] private CustomerSystem CustomerSystem;
        [field: SerializeField] private QueueSystem QueueSystem;
        [field: SerializeField] private SeatingSystem SeatingSystem;
        
        private readonly List<Action> ActiveActions = new();

        private IEnumerator CountDownCor;
        
        private void Start()
        {
            OnCookStart();
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();

            if (CountDownCor is not null)
            {
                StopCoroutine(CountDownCor);
                CountDownCor = null;
            }
        }

        #region State Machine
            #region OnCookStart
                private readonly StateMachine StateMachine = new();
                private void OnCookStart()
                {
                    StateMachine.ChangeState(new OnCookStart(OnEnter, OnExit));
                    return;
                    
                    void OnEnter()
                    {
                        CookMenuSystem.Show();
                        CookMenuSystem.OnClickOpenUIConfirmButton += OnRoundStart;
                        ActiveActions.Add(() => CookMenuSystem.OnClickOpenUIConfirmButton -= OnRoundStart);
                    }
                    
                    void OnExit()
                    {
                        CookMenuSystem.OnClickOpenUIConfirmButton -= OnRoundStart;
                    }
                }
            #endregion

            #region OnRoundStart
                private void OnRoundStart()
                {
                    StateMachine.ChangeState(new OnRoundStart(OnEnter, OnExit));
                    return;
                    
                    void OnEnter()
                    {
                        CountDownCor = CountDownCoroutine();
                        StartCoroutine(CountDownCor);
                        return;

                        IEnumerator CountDownCoroutine()
                        {
                            // TODO 暫時先用 true 來無限循環，之後再改成條件判斷
                            while (true)
                            {
                                QueueSystem.TryGetEmptyPoint(out var haveEmptyPoint, out var emptyPoint);
                                if (!haveEmptyPoint) { yield return new WaitForSeconds(8); continue; }
                                CustomerSystem.SpawnCustomer(out var customer);
                                emptyPoint.SetCustomer(customer);
                                customer.SetSkin();
                                customer.WalkToQueuePoint(emptyPoint);
                                yield return new WaitForSeconds(8);
                            }
                        }
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion
        #endregion
    }
}