using Card_Battle_System.System.Child;
using Card_Battle_System.System.Main.State_Machine;
using Card_Battle_System.System.Main.State_Machine.State;
using UnityEngine;

namespace Card_Battle_System.System.Main
{
    internal sealed class CardBattleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;
        
        [field: Header("Develop Only")]
        [field: SerializeField] private GameObject InitiativeSystem;
        
        private readonly StateMachine StateMachine = new();

        private void Start()
        {
            OnBattleStart();
        }
        
        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(OnBattleStart_Enter, OnBattleStart_Exit));
                }

                private void OnBattleStart_Enter()
                {
                    CardPoolSystem.Refill(() =>
                    {
                        CardPoolSystem.DrawCard(6, out var cards);
                        ShowCardSystem.ShowCard(cards, () =>
                        {
                            CardPoolSystem.Refill();
                            ShowCardSystem.GetShowCards(out var showCards);
                            HandCardSystem.Add(showCards, OnInitiativeCoin);
                        });
                    });
                }
                
                private void OnBattleStart_Exit()
                {
                }
            #endregion
            
            #region OnInitiativeCoin
                private void OnInitiativeCoin()
                {
                    StateMachine.ChangeState(new OnInitiativeCoin(OnInitiativeCoin_Enter, OnInitiativeCoin_Exit));
                }

                private void OnInitiativeCoin_Enter()
                {
                    // TODO 因為開發需求，日後改成生成 InitiativeSystem
                    InitiativeSystem.SetActive(true);
                }
                
                private void OnInitiativeCoin_Exit()
                {
                }
            #endregion
        #endregion
    }
}