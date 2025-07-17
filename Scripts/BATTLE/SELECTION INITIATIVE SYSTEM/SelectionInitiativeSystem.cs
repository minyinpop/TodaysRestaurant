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
        private Rect CanvasRect;
        
        [field: SerializeField] private RectTransform CoinParent;
        [field: SerializeField] private GameObject CoinPrefab;
        private GameObject Coin;

        /// <summary>
        /// 準備投擲硬幣的點，在介面上的 % 位置
        /// </summary>
        [field: SerializeField] private AnchorsMult ReadyPosMult;

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
            Coin.GetComponent<InitiativeCoin>().MoveToReadyPos(CanvasRect, ReadyPosMult);
        }
    }
}