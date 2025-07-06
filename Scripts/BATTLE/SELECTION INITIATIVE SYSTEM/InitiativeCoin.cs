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

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Interactable) return;

            RectTransform
                .DOScale(Vector2.one * 1.25f, .3f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Interactable) return;
            
            RectTransform
                .DOScale(Vector2.one, .3f)
                .SetEase(Ease.OutQuad);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable) return;
            Interactable = false;

            DOTween.Sequence()
                .Append(RectTransform
                    .DOScale(Vector2.one * 1.25f, .3f)
                    .SetEase(Ease.OutQuad))
                .AppendCallback(() =>
                {
                    var randomXPos = Random.Range(Screen.width * .25f, Screen.width * .75f);
                    var randomYPos = Random.Range(Screen.height * .5f, Screen.height * .8f);
                    
                    Debug.Log($"X: {randomXPos}, Y: {randomYPos}");
                    
                    // TODO 把硬幣移到目標位置
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