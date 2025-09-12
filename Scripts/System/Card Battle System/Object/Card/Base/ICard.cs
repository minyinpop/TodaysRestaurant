using Data.DOTween.Basic;
using Data.DOTween.Combine;
using UnityEngine;

namespace System.Card_Battle_System.Object.Card.Base
{
    internal interface ICard
    {
        /// <summary>
        /// 依照設定，把卡片移動到指定位置，並會在播放完畢後回傳
        /// </summary>
        /// <param name="parent">父物件的位置</param>
        /// <param name="settings">動畫參數</param>
        /// <param name="onComplete">完成後的回傳</param>
        public void MoveToParent(Transform parent, DoAnchorPos settings, Action onComplete = null);

        /// <summary>
        /// 移動卡片到指定的顯示位置，並且會展示卡片，在完畢後回傳
        /// </summary>
        /// <param name="parent">父物件的位置</param>
        /// <param name="anchorPosSettings">移動的動畫參數</param>
        /// <param name="flipSettings">翻轉的動畫參數</param>
        /// <param name="onComplete">完成後的回傳</param>
        public void MoveToShowPoint(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null);
    }
}