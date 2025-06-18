using BATTLE.OTHER;
using UnityEngine;

namespace BATTLE.SYSTEM.MAIN.TYPE
{
    [System.Serializable]
    internal class InitiativeSelectionSystem : MonoBehaviour
    {
        [field: Header("Initiative Coin")]
        [field: SerializeField] private RectTransform SpawnParent { get; set; }
        [field: SerializeField] private RectTransform ReadyParent { get; set; }
        [field: SerializeField] private RectTransform ShowParent { get; set; }
        [field: SerializeField] private GameObject InitiativeCoinPrefab { get; set; }
        
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
            InitiativeCoinObj = Instantiate(InitiativeCoinPrefab, SpawnParent);
            InitiativeCoinRect = InitiativeCoinObj.GetComponent<RectTransform>();
            InitiativeCoinScript = InitiativeCoinObj.GetComponent<InitiativeCoin>();
            
            InitiativeCoinScript.SlideInScreen(ReadyParent);
        }

        private void FinishFlip()
        {
            InitiativeCoinScript.SlideToMiddle(ShowParent);
        }
    }
}