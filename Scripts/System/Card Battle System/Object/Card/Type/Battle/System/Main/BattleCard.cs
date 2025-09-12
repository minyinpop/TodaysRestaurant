using System.Card_Battle_System.Object.Card.Base;
using System.Card_Battle_System.Object.Card.Type.Battle.System.Child;
using Data.Card.Battle;
using Data.DOTween.Basic;
using Data.DOTween.Combine;
using DG.Tweening;
using UnityEngine;

namespace System.Card_Battle_System.Object.Card.Type.Battle.System.Main
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

        #region ICard
            /// <summary>
            /// 設定卡片的父物件，並呼叫動畫系統，執行移動的動畫，在結束後回傳
            /// </summary>
            /// <param name="parent">父物件的位置</param>
            /// <param name="settings">動畫參數</param>
            /// <param name="onComplete">完成後的回傳</param>
            public void MoveToParent(Transform parent, DoAnchorPos settings, Action onComplete = null)
            {
                Rect.SetParent(parent);
                AnimationSystem.MoveTo(settings)
                    .OnComplete(() => onComplete?.Invoke());
            }

            /// <summary>
            /// 設定卡片的父物件，並呼叫動畫系統，執行移動的動畫，接著播放翻轉到正面的動畫，在結束後回傳
            /// </summary>
            /// <param name="parent">父物件的位置</param>
            /// <param name="anchorPosSettings">移動的動畫參數</param>
            /// <param name="flipSettings">翻轉的動畫參數</param>
            /// <param name="onComplete">完成後的回傳</param>
            public void MoveToShowPoint(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null)
            {
                Rect.SetParent(parent);
                
                flipSettings.GetValues(out var rotateSettings, out var scaleSettings01, out var scaleSettings02);
                
                AnimationSystem.MoveTo(anchorPosSettings)
                    .OnComplete(() => AnimationSystem.FlipToFront(rotateSettings, scaleSettings01, scaleSettings02)
                        .OnComplete(() => onComplete?.Invoke()));
            }
        #endregion
    }
}