using System;
using System.Collections.Generic;
using Battle_Management_System.Card_System.Card_Pool_System;
using Battle_Management_System.Card_System.Hand_Card_System;
using Battle_Management_System.Card_System.Show_Card_System;
using Battle_Management_System.Card_System.State_Machine;
using Battle_Management_System.Card_System.State_Type;
using UnityEngine;

namespace Battle_Management_System.Card_System
{
    internal class CardSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;

        [field: Range(1, 5)] public int CardNumber;

        private readonly StateMachine StateMachine = new();

        private void Start()
        {
            OnBattleStart();
        }
        
        #region State Machine
            private void ChangeState(IState newState)
            {
                StateMachine.ChangeState(newState);
            }

            private void ExitState()
            {
                StateMachine.Exit();
            }

            #region On Battle Start
                private void OnBattleStart()
                {
                    ChangeState(new OnBattleStart(EnterOnBattleStart, ExitOnBattleStart));
                }

                private void EnterOnBattleStart()
                {
                    RefillCard(() =>
                    {
                        GetCard(CardNumber, out var cardList);
                        ShowCard(cardList, () => RefillCard(), () =>
                        {
                            GetAllShowCard(out cardList);
                            AddCardToHand(cardList, ExitState);
                        });
                    });
                }

                private void ExitOnBattleStart()
                {
                    OnDecisiveCoin();
                }
            #endregion
            
            #region On Decisive Coin
                private void OnDecisiveCoin()
                {
                    ChangeState(new OnDecisiveCoin(EnterOnDecisiveCoin, ExitOnDecisiveCoin));
                }

                private void EnterOnDecisiveCoin()
                {
                    Debug.Log("Enter On Decisive Coin State");
                }
                
                private void ExitOnDecisiveCoin()
                {
                }
            #endregion
        #endregion
        
        #region Card Pool System
            private void RefillCard(Action onComplete = null)
            {
                CardPoolSystem.RefillCard(onComplete);
            }

            private void GetCard(int number, out List<GameObject> cardList)
            {
                CardPoolSystem.GetCard(number, out cardList);
            }
        #endregion
        
        #region Show Card System
            private void ShowCard(List<GameObject> cardList, Action onGetComplete = null, Action onShowComplete = null)
            {
                ShowCardSystem.ShowCard(cardList, onGetComplete, onShowComplete);
            }

            private void GetAllShowCard(out List<GameObject> cardList)
            {
                ShowCardSystem.GetCards(out cardList);
            }
        #endregion
        
        #region Hand Card System
            private void AddCardToHand(List<GameObject> cardList, Action onComplete = null)
            {
                HandCardSystem.AddCard(cardList, onComplete);
            }
        #endregion
    }
}