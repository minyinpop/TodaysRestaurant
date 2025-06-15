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
        [field: SerializeField] private GameObject InitiativeCoinPrefab { get; set; }
        
        private GameObject InitiativeCoin { get; set; }
        private RectTransform InitiativeCoinRect { get; set; }
        private InitiativeCoin InitiativeCoinScript { get; set; }

        public void Init()
        {
            InitiativeCoin = Instantiate(InitiativeCoinPrefab, SpawnParent);
            InitiativeCoinRect = InitiativeCoin.GetComponent<RectTransform>();
            InitiativeCoinScript = InitiativeCoin.GetComponent<InitiativeCoin>();

            InitiativeCoinScript.SlideInScreen();
        }
    }
}