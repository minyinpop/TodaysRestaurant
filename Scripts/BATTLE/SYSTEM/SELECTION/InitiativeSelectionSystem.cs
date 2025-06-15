using BATTLE.COIN;
using BATTLE.STATE_MACHINE.CATEGORY;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.SYSTEM.SELECTION
{
    internal class InitiativeSelectionSystem : MonoBehaviour
    {
        [field: Header("Mask")]
        [field: SerializeField] private CanvasGroup ScreenMask { get; set; }
        
        [field: Header("Settings")]
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
            DOTween.Sequence()
                .Append(ScreenMask.DOFade(1, 1))
                .AppendCallback(() =>
                {
                    CoinSettings.Coin = Instantiate(CoinSettings.CoinPrefab, CoinSettings.SpawnParent);
                    CoinSettings.CoinRect = CoinSettings.Coin.GetComponent<RectTransform>();
                    CoinSettings.CoinComponent = CoinSettings.Coin.GetComponent<Coin>();
                })
                .AppendCallback(() =>
                {
                    CoinSettings.CoinComponent.Init(CoinSettings.ShowParent);
                    CoinSettings.CoinComponent.OnShowCompleteEvent += InitiativeSelection;
                });
        }

        private void InitiativeSelection(bool isPlayerFirst)
        {
            var topContent = isPlayerFirst ? TextSettings.Heads.TopContent : TextSettings.Tails.TopContent;
            var bottomContent = isPlayerFirst ? TextSettings.Heads.BottomContent : TextSettings.Tails.BottomContent;
            DOTween.Sequence()
                .AppendInterval(.5f)
                .AppendCallback(() =>
                {
                    TextSettings.TopTMP.text = topContent;
                })
                .AppendInterval(1)
                .AppendCallback(() =>
                {
                    TextSettings.BottomTMP.text = bottomContent;
                })
                .AppendInterval(3)
                .AppendCallback(() =>
                {
                    ScreenMask.DOFade(0, 1);
                });
        }
    }
}