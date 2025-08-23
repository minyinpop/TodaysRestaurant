using System;
using System.Collections.Generic;
using Battle_Management_System.Card_System.Card_Pool_System;
using Battle_Management_System.Card_System.Show_Card_System;
using Battle_Management_System.Hand_Card_System;
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

        public void RefillCardAndDrawOnBattleStart(Action onComplete)
        {
            RefillCard(() =>
            {
                GetCard(CardNumber, out var cardList);
                ShowCard(cardList, () => RefillCard(), () =>
                {
                    GetAllShowCard(out cardList);
                    AddCardToHand(cardList, onComplete.Invoke);
                });
            });
        }
        
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