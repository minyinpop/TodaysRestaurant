using BATTLE.OTHER;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BATTLE.SYSTEM.MAIN.TYPE
{
    [System.Serializable]
    internal class InitiativeSelectionSystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private CanvasGroup FullScreenMask { get; set; }
        
        [field: Header("Settings")]
        [field: SerializeField] private InitiativeCoinSettings InitiativeCoinSettings { get; set; }
        [field: SerializeField] private ResultTextSettings ResultTextSettings { get; set; }
        
        private GameObject InitiativeCoinObj { get; set; }
        private RectTransform InitiativeCoinRect { get; set; }
        private InitiativeCoin InitiativeCoinScript { get; set; }

        private void OnEnable()
        {
            InitiativeCoin.FinishFlipEvent += FinishFlip;
        }
        
        private void OnDisable()
        {
            InitiativeCoin.FinishFlipEvent -= FinishFlip;
        }

        public void Init()
        {
            DOTween.Sequence()
                .Append(FullScreenMask.DOFade(1, 1))
                .AppendCallback(() =>
                {
                    InitiativeCoinObj = Instantiate(InitiativeCoinSettings.InitiativeCoinPrefab, InitiativeCoinSettings.SpawnParent);
                    InitiativeCoinRect = InitiativeCoinObj.GetComponent<RectTransform>();
                    InitiativeCoinScript = InitiativeCoinObj.GetComponent<InitiativeCoin>();

                    InitiativeCoinScript.Init(InitiativeCoinSettings.ReadyParent, InitiativeCoinSettings.ShowParent);
                });
        }

        /// <summary>
        /// Handles the completion of the coin flip and updates the user interface accordingly.
        /// </summary>
        /// <param name="isHeads"> A boolean indicating whether the coin flip resulted in heads (true) or tails (false). </param>
        private void FinishFlip(bool isHeads)
        {
            DOTween.Sequence()
                .AppendCallback(() => { ResultTextSettings.SetText(isHeads); })
                .AppendInterval(3)
                .Append(InitiativeCoinRect
                    .DOScale(Vector2.zero, .5f)
                    .SetEase(Ease.InOutBack))
                .JoinCallback(() => { ResultTextSettings.ClearText(); })
                .AppendCallback(() => { FullScreenMask.DOFade(0, 1); })
                .OnComplete(() =>
                {
                    // TODO What will happen when player finish to flip initiative coin?
                });
        }
    }

    [System.Serializable]
    internal class InitiativeCoinSettings
    {
        [field: SerializeField] public RectTransform SpawnParent { get; private set; }
        [field: SerializeField] public RectTransform ReadyParent { get; private set; }
        [field: SerializeField] public RectTransform ShowParent { get; private set; }
        [field: SerializeField] public GameObject InitiativeCoinPrefab { get; private set; }
    }

    [System.Serializable]
    internal class ResultTextSettings
    {
        [field: Header("Text")]
        [field: SerializeField] public TextMeshProUGUI TopTMP { get; private set; }
        [field: SerializeField] public TextMeshProUGUI BottomTMP { get; private set; }
        
        [field: Header("Color")]
        [field: SerializeField] public ContentSettings HeadsSettings { get; private set; }
        [field: SerializeField] public ContentSettings TailsSettings { get; private set; }

        public void SetText(bool isHeads)
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    TopTMP.text = isHeads ? HeadsSettings.TopContent : TailsSettings.TopContent;
                    TopTMP.color = isHeads ? HeadsSettings.TextColor : TailsSettings.TextColor;
                })
                .AppendInterval(1)
                .AppendCallback(() =>
                {
                    BottomTMP.text = isHeads ? HeadsSettings.BottomContent : TailsSettings.BottomContent;
                    BottomTMP.color = isHeads ? HeadsSettings.TextColor : TailsSettings.TextColor;
                });
        }

        public void ClearText()
        {
            TopTMP.text = "";
            BottomTMP.text = "";
        }
    }

    [System.Serializable]
    internal class ContentSettings
    {
        [field: SerializeField] public string TopContent { get; private set; }
        [field: SerializeField] public string BottomContent { get; private set; }
        [field: SerializeField] public Color TextColor { get; private set; }
    }
}