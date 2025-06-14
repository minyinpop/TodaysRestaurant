using BATTLE.COIN;
using BATTLE.STATE_MACHINE.CATEGORY;
using UnityEngine;

namespace BATTLE.SYSTEM.SELECTION
{
    internal class InitiativeSelectionSystem : MonoBehaviour
    {
        [field: SerializeField] private CoinSettings CoinSettings { get; set; }
        [field: SerializeField] private TextSettings TextSettings { get; set; }

        private void OnEnable()
        {
            InitiativeSelectionState.OnEnterEvent += SpawnCoin;
        }

        private void OnDisable()
        {
            InitiativeSelectionState.OnEnterEvent -= SpawnCoin;
            
            try
            {
                CoinSettings.CoinComponent.OnShowCompleteEvent -= InitiativeSelection;
            }
            catch (System.Exception)
            {
                Debug.Log("CoinComponent is null");
            }
        }

        private void SpawnCoin()
        {
            CoinSettings.Coin = Instantiate(CoinSettings.CoinPrefab, CoinSettings.SpawnParent);
            CoinSettings.CoinComponent = CoinSettings.Coin.GetComponent<Coin>();
            CoinSettings.CoinComponent.Init(CoinSettings.ShowParent);
            CoinSettings.CoinComponent.OnShowCompleteEvent += InitiativeSelection;
        }

        private void InitiativeSelection(bool isPlayerFirst)
        {
            if (isPlayerFirst)
            {
                TextSettings.Heads.TopTMP.text = TextSettings.Heads.TopContent;
                TextSettings.Heads.BottomTMP.text = TextSettings.Heads.BottomContent;
            }
            else
            {
                TextSettings.Tails.TopTMP.text = TextSettings.Tails.TopContent;
                TextSettings.Tails.BottomTMP.text = TextSettings.Tails.BottomContent;
            }
        }
    }
}