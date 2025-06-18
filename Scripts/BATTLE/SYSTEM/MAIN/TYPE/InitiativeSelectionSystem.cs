using BATTLE.OTHER;
using TMPro;
using UnityEngine;

namespace BATTLE.SYSTEM.MAIN.TYPE
{
    [System.Serializable]
    internal class InitiativeSelectionSystem : MonoBehaviour
    {
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
            InitiativeCoinObj = Instantiate(InitiativeCoinSettings.InitiativeCoinPrefab, InitiativeCoinSettings.SpawnParent);
            InitiativeCoinRect = InitiativeCoinObj.GetComponent<RectTransform>();
            InitiativeCoinScript = InitiativeCoinObj.GetComponent<InitiativeCoin>();
            
            InitiativeCoinScript.Init(InitiativeCoinSettings.ReadyParent, InitiativeCoinSettings.ShowParent);;
        }

        private void FinishFlip(bool isHeads)
        {
            ResultTextSettings.SetText(isHeads);
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
            TopTMP.text = isHeads ? HeadsSettings.TopContent : TailsSettings.TopContent;
            BottomTMP.text = isHeads ? HeadsSettings.BottomContent : TailsSettings.BottomContent;
            TopTMP.color = isHeads ? HeadsSettings.TextColor : TailsSettings.TextColor;
            BottomTMP.color = isHeads ? HeadsSettings.TextColor : TailsSettings.TextColor;
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