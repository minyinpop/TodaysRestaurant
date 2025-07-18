using UnityEngine;
using UTILITY;

namespace BATTLE.SELECTION_INITIATIVE_SYSTEM
{
    internal class SelectionInitiativeSystem : MonoBehaviour
    {
        /// <summary>
        /// 硬幣所在的介面
        /// </summary>
        [field: SerializeField] private Canvas Canvas;
        /// <summary>
        /// 介面的大小
        /// </summary>
        private Rect CanvasRect;
        
        /// <summary>
        /// 硬幣的父物件
        /// </summary>
        [field: SerializeField] private RectTransform CoinParent;
        /// <summary>
        /// 硬幣的預製件
        /// </summary>
        [field: SerializeField] private GameObject CoinPrefab;
        /// <summary>
        /// 用來暫存硬幣的遊戲物件
        /// </summary>
        private GameObject Coin;
        /// <summary>
        /// 硬幣的 InitiativeCoin 程式碼
        /// </summary>
        private InitiativeCoin CoinScript;

        /// <summary>
        /// 準備投擲硬幣的點，在介面上的 % 位置
        /// </summary>
        [field: SerializeField] private AnchorsMult ReadyPosMult;
        /// <summary>
        /// 當硬幣被點擊後，所落下的介面 % 位置
        /// </summary>
        [field: SerializeField] private AnchorsMult LandingPosMult;

        private void Awake()
        {
            CanvasRect = Canvas.GetComponent<RectTransform>().rect;
        }

        /// <summary>
        /// 生成硬幣
        /// </summary>
        public void SpawnCoin()
        {
            var spawnPos = new Vector2(CanvasRect.width * .5f, CanvasRect.height * -.3f);
            Coin = Instantiate(CoinPrefab, spawnPos, Quaternion.identity, CoinParent);
            CoinScript = Coin.GetComponent<InitiativeCoin>();
            CoinScript.Init(CanvasRect, ReadyPosMult, LandingPosMult);
            CoinScript.MoveToReadyPos();
        }
    }
}