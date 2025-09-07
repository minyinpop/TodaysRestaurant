using System;
using Data.DOTween;
using UnityEngine;

namespace Card_Battle_System.Object.Card.Base
{
    internal interface ICard
    {
        /// <summary>
        /// 依照設定，把卡片移動到指定位置，並會在播放完畢後回傳
        /// </summary>
        /// <param name="parent">父物件的位置</param>
        /// <param name="settings">移動的動畫參數</param>
        /// <param name="onComplete">完成後的回傳</param>
        public void MoveToParent(Transform parent, DoAnchorPos settings, Action onComplete = null);
    }
}