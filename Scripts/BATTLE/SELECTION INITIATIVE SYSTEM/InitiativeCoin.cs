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
        [field: SerializeField] private AnchorsMult ReadyPosMult;
        /// <summary>
        /// 當硬幣被點擊後，所落下的介面 % 位置
        /// </summary>
        [field: SerializeField] private AnchorsMult LandingPosMult;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
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

            // 螢幕的寬度 * 螢幕寬度的比例位置
            var posX = CanvasRect.width * LandingPosMult.GetRandomXMult();
            // 螢幕的高度 * 螢幕高度的比例位置
            var posY = CanvasRect.height * LandingPosMult.GetRandomYMult();
            
            // 不轉動 X 軸
            var rotateX = RectTransform.eulerAngles.x;
            // 翻轉到另一個面所需要的角度 * 翻轉幾次
            var rotateY = 180 * Random.Range(12, 24);
            // 最後的隨機角度 * 旋轉幾次
            var rotateZ = Random.Range(0, 361) * Random.Range(12, 24);

            DOTween.Sequence()
                .Append(RectTransform
                    .DOAnchorPos(new Vector2(posX, posY), 5, true)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOLocalRotate(new Vector3(rotateX, rotateY, rotateZ), 5, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOScale(Vector2.one, 5)
                    .SetEase(Ease.OutQuint));
            
            // TODO 07.18 當硬幣的 Y 軸翻轉過 180 後，要切換到另一個面
        }

        /// <summary>
        /// 用來初始化硬幣的方法
        /// </summary>
        /// <param name="rect"> 硬幣所在介面的像素大小 </param>
        /// <param name="readyPosMult"> 準備投擲硬幣的點，在介面上的 % 位置 </param>
        /// <param name="landingPosMult"> 當硬幣被點擊後，所落下的介面 % 位置 </param>
        public void Init(Rect rect, AnchorsMult readyPosMult, AnchorsMult landingPosMult)
        {
            CanvasRect = rect;
            ReadyPosMult = readyPosMult;
            LandingPosMult = landingPosMult;
        }

        /// <summary>
        /// 用來執行硬幣從生成點移動到準備投擲點的方法
        /// </summary>
        public void MoveToReadyPos()
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
    }
}