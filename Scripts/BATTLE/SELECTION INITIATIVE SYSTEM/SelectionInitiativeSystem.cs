using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using DG.Tweening;
using UnityEngine;
using UTILITY;

namespace BATTLE.SELECTION_INITIATIVE_SYSTEM
{
    internal class SelectionInitiativeSystem : MonoBehaviour
    {
        /// <summary>
        /// 遮蔽介面的遊戲物件，用於讓玩家聚焦在硬幣的互動上
        /// </summary>
        [field: Header("介面")]
        [field: SerializeField] private CanvasGroup FullScreenMask;
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
        [field: Header("硬幣")]
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
        [field: Header("位置")]
        [field: SerializeField] private AnchorsMult ReadyPosMult;
        /// <summary>
        /// 當硬幣被點擊後，所落下的介面 % 位置
        /// </summary>
        [field: SerializeField] private AnchorsMult LandingPosMult;
        /// <summary>
        /// 當硬幣結束投擲後，所移動到顯示結果的介面 % 位置
        /// </summary>
        [field: SerializeField] private AnchorsMult ShowPosMult;

        private void Awake()
        {
            CanvasRect = Canvas.GetComponent<RectTransform>().rect;
        }

        /// <summary>
        /// 當進入戰鬥後，管理戰鬥進程的主系統，就會呼叫這個 Method
        /// 負責生成決定玩家或是敵人先手的硬幣
        /// </summary>
        public void OnEnter()
        {
            var spawnPos = new Vector2(CanvasRect.width * .5f, CanvasRect.height * -.3f);
            
            Coin = Instantiate(CoinPrefab, spawnPos, Quaternion.identity, CoinParent);
            CoinScript = Coin.GetComponent<InitiativeCoin>();
            
            DOTween.Sequence()
                .Append(FullScreenMask
                    .DOFade(1, 1)
                    .SetEase(Ease.OutQuad))
                .OnKill(() =>
                {
                    CoinScript.Finish += CoinOnShowPos;
                    CoinScript.Init(CanvasRect, ReadyPosMult, LandingPosMult, ShowPosMult);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whoFirst"></param>
        private void CoinOnShowPos(IState whoFirst)
        {
            
        }
    }
}