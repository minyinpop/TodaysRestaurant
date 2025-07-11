using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.PROGRESSING_SYSTEM.STATE;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

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
            
            var randomXPos = Random.Range(Screen.width * .25f, Screen.width * .75f);
            var randomYPos = Random.Range(Screen.height * .5f, Screen.height * .8f);

            var randomYTurn = 180 * Random.Range(12, 25);
            var randomZTurn = Random.Range(0, 361) * Random.Range(12, 25);

            DOTween.Sequence()
                .Append(RectTransform
                    .DOAnchorPos(new Vector2(randomXPos, randomYPos), 5, true)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DORotate(new Vector3(RectTransform.eulerAngles.x, randomYTurn, randomZTurn), 5,
                        RotateMode.FastBeyond360)
                    .SetEase(Ease.OutQuad))
                .Join(RectTransform
                    .DOScale(Vector2.one * 1.25f, 3)
                    .SetEase(Ease.InOutBack))
                .OnUpdate(() =>
                {
                    var RotY = RectTransform.eulerAngles.y;
                    if (Heads.activeSelf && RotY is <= 90 and >= 0)
                    {
                        Debug.Log("翻到背面");
                        Heads.SetActive(false);
                        Tails.SetActive(true);
                    }
                    else if (Tails.activeSelf && RotY is >= 270 or <= 0)
                    {
                        Debug.Log("翻到正面");
                        Heads.SetActive(true);
                        Tails.SetActive(false);
                    }
                })
                .OnComplete(() =>
                {
                    Debug.Log("硬幣翻轉結束");
                })
                .OnKill(() =>
                {
                    Debug.Log("硬幣被殺死了");
                });
        }
        
        
        
        /// <summary>
        /// 移動硬幣到準備投擲的位置
        /// </summary>
        /// <param name="targetPos"> 目標點的位置 </param>
        public void SetPosToReady(RectTransform targetPos)
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    RectTransform.SetParent(targetPos);
                })
                .Append(RectTransform
                    .DOAnchorPos(Vector2.zero, 1, true)
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
            
        }
    }
}