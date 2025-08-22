using Battle_Management_System.Card_Pool_System;
using Battle_Management_System.Show_Card_System;
using Battle_Management_System.State_Machine;
using Battle_Management_System.State_Type;
using UnityEngine;

namespace Battle_Management_System
{
    internal class BattleManagementSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;

        [field: Range(1, 5)] public int CardNumber;

        private readonly StateMachine StateMachine = new();

        private void Awake()
        {
            OnRefillCardWhenBattleStart();
        }
        
        #region State Machine
            private void ChangeState(IState newState) => StateMachine.ChangeState(newState);
            private void ExitState() => StateMachine.Exit();
            
            private void OnRefillCardWhenBattleStart() => ChangeState(new OnRefillCardWhenBattleStart(RefillCard, OnDrawCardWhenBattleStart));
            private void OnDrawCardWhenBattleStart() => ChangeState(new OnDrawCardWhenBattleStart(() => DrawCard(CardNumber), () => Debug.Log("抽完卡片並展示了")));
        #endregion
        
        #region Card Pool System
            private void RefillCard() => CardPoolSystem.RefillCard(ExitState);
        #endregion
        
        #region Draw Card System
            private void DrawCard(int number)
            {
                CardPoolSystem.GetCard(number, out var cardList);
                ShowCardSystem.ShowCard(cardList, ExitState);
            }
        #endregion
    }
}