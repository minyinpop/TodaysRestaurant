using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.PROGRESSING_SYSTEM.STATE;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UTILITY;

namespace BATTLE.SELECTION_INITIATIVE_SYSTEM
{
    /// <summary>
    /// 用於執行決定玩家或是敵人，哪一方先手的程式碼
    /// </summary>
    internal class InitiativeCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private RectTransform RectTransform;
        
        [field: SerializeField] private GameObject Heads;
        [field: SerializeField] private GameObject Tails;
        
        [field: SerializeField, Range(0, 1)] public float widthMultiplier;
        [field: SerializeField, Range(0, 1)] public float heightMultiplier;

        /// <summary>
        /// 用來控制硬幣是否可以被點擊
        /// </summary>
        private bool Interactable;
        
        /// <summary>
        /// 當玩家完成投擲，並且展示了結果後，就會觸發這個 Broadcast
        /// 用於告訴控制戰鬥進程的系統，由玩家或是敵人先手
        /// </summary>
        public event System.Action<IState> Finish;

        /// <summary>
        /// 用於直接性的控制硬幣縮放
        /// </summary>
        private Tween ScaleTween;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Interactable) return;

            ScaleTween.Kill();
            ScaleTween = null;

            ScaleTween = RectTransform
                .DOScale(Vector2.one * 1.25f, .3f)
                .SetEase(Ease.OutQuad);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Interactable) return;
            
            ScaleTween.Kill();
            ScaleTween = null;

            ScaleTween = RectTransform
                .DOScale(Vector2.one, .3f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable) return;
            Interactable = false;

            ScaleTween.Kill();
            ScaleTween = null;
            
            // 螢幕寬度的 1/4 * 左邊或是右邊
            var randomXPos = Screen.width * widthMultiplier; /*Random.Range(1, Screen.width * .25f) * Random.Range(0, 2) * 2 - 1*/
            var randomYPos = Screen.height * heightMultiplier;

            var randomYRollCount = Random.Range(12, 25);
            var randomZRollCount = Random.Range(12, 25);

            var angleY = 180 * randomYRollCount;
            var angleZ = Random.Range(0, 361) * randomZRollCount;

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    RectTransform.localScale = Vector2.one * 1.25f;
                })
                .Append(RectTransform
                    .DOAnchorPos(new Vector2(randomXPos, randomYPos), 3, true)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DORotate(new Vector3(RectTransform.eulerAngles.x, angleY, angleZ), 3,
                        RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOScale(Vector2.one, 3)
                    .SetEase(Ease.InOutBack))
                .AppendInterval(.5f)
                .OnUpdate(() =>
                {
                    var rotY = RectTransform.eulerAngles.y;

                    if (Heads.activeSelf && rotY is < 270 and > 90)
                    {
                        Heads.SetActive(false);
                        Tails.SetActive(true);
                    }
                    else if (Tails.activeSelf && rotY is < 360 and > 270)
                    {
                        Heads.SetActive(true);
                        Tails.SetActive(false);
                    }
                })
                .OnComplete(() =>
                {
                    Finish?.Invoke(randomYRollCount % 2 == 0 ? new OnPlayerRound() : new OnEnemyRound());
                });
        }


        /// <summary>
        /// 移動硬幣到準備投擲的位置
        /// </summary>
        /// <param name="targetPosMult"> 目標點的位置乘數 </param>
        public void SetPosToReady(Canvas canvas, AnchorsMult targetPosMult)
        {
            var rect = canvas.GetComponent<RectTransform>().rect;
            
            var readyPosX = rect.width * .5f;
            var readyPosY = rect.height * .15f;
            var readyPos = new Vector2(readyPosX, readyPosY);
            
            DOTween.Sequence()
                .Append(RectTransform
                    .DOAnchorPos(readyPos, 1, true)
                    .SetEase(Ease.OutBack))
                .OnComplete(() =>
                {
                    Interactable = true;
                });
        }

        /// <summary>
        /// 移動硬幣到顯示結果的位置
        /// </summary>
        /// <param name="targetPos"> 目標點的位置 </param>
        public void SetPosToShow(RectTransform targetPos)
        {
            // var midXPos = Screen.width 
            // DOTween.Sequence()
            //     .Append(RectTransform
            //         .DOAnchorPos(Screen.width))
        }
    }
}