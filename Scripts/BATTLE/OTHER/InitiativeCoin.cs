using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BATTLE.OTHER
{
    internal class InitiativeCoin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        // Components
        private RectTransform RectTransform { get; set; }
        
        // State
        private bool CanClick { get; set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void SlideInScreen(RectTransform parent)
        {
            RectTransform.SetParent(parent);

            RectTransform
                .DOAnchorPos(Vector2.zero, .75f, true)
                .OnComplete(() => { CanClick = true; });
        }

        #region Pointer Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!CanClick) return;
            RectTransform.DOScale(Vector2.one * 2, .25f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!CanClick) return;
            RectTransform.DOScale(Vector2.one, .25f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanClick) return;
            CanClick = false;
            
            var randomDistanceX = Screen.width * Random.Range(-.25f, .25f);
            var randomDistanceY = Screen.height * Random.Range(.1f, .5f);
        }
        #endregion
    }
}