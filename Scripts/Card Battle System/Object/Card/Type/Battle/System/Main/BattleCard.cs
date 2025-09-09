using System;
using Card_Battle_System.Object.Card.Base;
using Card_Battle_System.Object.Card.Type.Battle.System.Child;
using Data.Card.Battle;
using Data.DOTween;
using UnityEngine;

namespace Card_Battle_System.Object.Card.Type.Battle.System.Main
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCard : MonoBehaviour, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform Rect;
        
        [field: Header("Data")]
        [field: SerializeField] private BattleCardSO BattleCardData;
        
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;

        /// <summary>
        /// 獲取該卡片被抽到的機率
        /// </summary>
        /// <param name="chance">回傳被抽到的機率</param>
        public void GetDrawChance(out float chance)
        {
            BattleCardData.GetDrawChance(out var drawChance);
            chance = drawChance;
        }

        /// <summary>
        /// 設定卡片的父物件，並呼叫動畫系統，執行移動的動畫，並會在播放完畢後回傳
        /// </summary>
        /// <param name="parent">父物件的位置</param>
        /// <param name="settings">移動的動畫參數</param>
        /// <param name="onComplete">完成後的回傳</param>
        public void MoveToParent(Transform parent, DoAnchorPos settings, Action onComplete = null)
        {
            Rect.SetParent(parent);
            AnimationSystem.MoveTo(settings, onComplete);
        }
    }
}