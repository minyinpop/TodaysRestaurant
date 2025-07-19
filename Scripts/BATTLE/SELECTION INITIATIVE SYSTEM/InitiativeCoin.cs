using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.PROGRESSING_SYSTEM.STATE;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UTILITY;

namespace BATTLE.SELECTION_INITIATIVE_SYSTEM
{
    internal class InitiativeCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// 自身的 RectTransform Component
        /// </summary>
        private RectTransform RectTransform;
        
        /// <summary>
        /// 介面的大小
        /// </summary>
        private Rect CanvasRect;
        
        /// <summary>
        /// 用來判斷滑鼠是否可以與自身互動
        /// </summary>
        private bool Interactable;
        
        /// <summary>
        /// 準備投擲硬幣的點，在介面上的 % 位置
        /// </summary>
        private AnchorsMult ReadyPosMult;
        /// <summary>
        /// 當硬幣被點擊後，所落下的介面 % 位置
        /// </summary>
        private AnchorsMult LandingPosMult;
        /// <summary>
        /// 當硬幣結束投擲後，所移動到顯示結果的介面 % 位置
        /// </summary>
        private AnchorsMult ShowPosMult;

        /// <summary>
        /// 硬幣正面的遊戲物件
        /// </summary>
        [field: SerializeField] private GameObject Heads;
        /// <summary>
        /// 硬幣反面的遊戲物件
        /// </summary>
        [field: SerializeField] private GameObject Tails;

        /// <summary>
        /// 當玩家投擲硬幣，並且硬幣結束翻轉及移動，就會觸發這個廣播
        /// 這個廣播會把投擲結果給發送出去
        /// </summary>
        public event System.Action<IState> Finish;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
        
        /// <summary>
        /// 用來初始化硬幣的方法
        /// </summary>
        /// <param name="rect"> 硬幣所在介面的像素大小 </param>
        /// <param name="readyPosMult"> 準備投擲硬幣的點，在介面上的 % 位置 </param>
        /// <param name="landingPosMult"> 當硬幣被點擊後，所落下的介面 % 位置 </param>
        /// <param name="showPosMult"> 當硬幣結束投擲後，所移動到顯示結果的介面 % 位置 </param>
        public void Init(Rect rect, AnchorsMult readyPosMult, AnchorsMult landingPosMult, AnchorsMult showPosMult)
        {
            CanvasRect = rect;
            ReadyPosMult = readyPosMult;
            LandingPosMult = landingPosMult;
            ShowPosMult  = showPosMult;
            
            MoveToReadyPos();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Interactable) return;

            var size = Vector2.one * 1.2f;
            RectTransform
                .DOScale(size, .3f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Interactable) return;

            var size = Vector2.one;
            RectTransform
                .DOScale(size, .3f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable) return;
            Interactable = false;

            DOTween.Sequence()
                .Append(MoveToRandomPos())
                .AppendInterval(.5f)
                .Append(MoveToShowPos())
                .AppendInterval(.5f)
                .OnKill(() =>
                {
                    // TODO 廣播硬幣投擲結果
                    Debug.Log("廣播硬幣投擲結果");
                });
        }

        /// <summary>
        /// 用來執行硬幣從生成點移動到準備投擲點的方法
        /// </summary>
        private void MoveToReadyPos()
        {
            var x = CanvasRect.width * ReadyPosMult.GetRandomXMult();
            var y = CanvasRect.height * ReadyPosMult.GetRandomYMult();
            
            RectTransform
                .DOAnchorPos(new Vector2(x, y), 1, true)
                .SetEase(Ease.OutBack)
                .OnKill(() =>
                {
                    Interactable = true;
                });
        }

        /// <summary>
        /// 用來執行當玩家點擊硬幣後，會移動到螢幕上隨機 % 的位置
        /// </summary>
        private Tween MoveToRandomPos()
        {
            // 螢幕的寬度 * 螢幕寬度的比例位置
            var posX = CanvasRect.width * LandingPosMult.GetRandomXMult();
            // 螢幕的高度 * 螢幕高度的比例位置
            var posY = CanvasRect.height * LandingPosMult.GetRandomYMult();

            // Y 軸的翻面次數
            var rotateYCount = Random.Range(24, 32);
            // Z 軸的旋轉次數
            var rotateZCount = Random.Range(12, 24);
            
            // 不轉動 X 軸
            var rotateX = RectTransform.eulerAngles.x;
            // 翻轉到另一個面所需要的角度 * 翻轉幾次
            var rotateY = 180 * rotateYCount;
            // 最後的隨機角度 * 旋轉幾次
            var rotateZ = Random.Range(0, 361) * rotateZCount;

            return DOTween.Sequence()
                .Append(RectTransform
                    .DOAnchorPos(new Vector2(posX, posY), 5, true)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOLocalRotate(new Vector3(rotateX, rotateY, rotateZ), 5, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOScale(Vector2.one, 5)
                    .SetEase(Ease.OutQuint))
                .OnUpdate(() =>
                {
                    if (!Tails.activeSelf && RectTransform.eulerAngles.y is < 100 and > 90)
                    {
                        Heads.SetActive(false);
                        Tails.SetActive(true);
                    }
                    else if (!Heads.activeSelf && RectTransform.eulerAngles.y is < 280 and > 270)
                    {
                        Heads.SetActive(true);
                        Tails.SetActive(false);
                    }
                });
        }

        /// <summary>
        /// 讓硬幣移動到營顯示位置，並且放大，讓玩家看得更清楚結果
        /// </summary>
        private Tween MoveToShowPos()
        {
            var x = CanvasRect.width * ShowPosMult.GetRandomXMult();
            var y = CanvasRect.height * ShowPosMult.GetRandomYMult();

            return DOTween.Sequence()
                .Append(RectTransform
                    .DOAnchorPos(new Vector2(x, y), .3f, true)
                    .SetEase(Ease.OutBack))
                .Append(RectTransform
                    .DOScale(Vector2.one * 1.5f, .3f)
                    .SetEase(Ease.InOutBack));
        }
    }
}