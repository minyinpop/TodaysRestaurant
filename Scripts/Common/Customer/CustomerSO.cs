using System.Collections.Generic;
using Common.Value;
using Spine;
using UnityEngine;

namespace Common.Customer
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Customer", fileName = "New Data")]
    internal sealed class CustomerSO : ScriptableObject
    {
        [field: Header("查看菜單")]
        [field: SerializeField] private float watchFoodMenuDuration;
        public float WatchFoodMenuDuration => watchFoodMenuDuration;
        
        [field: Header("等待點餐")]
        [field: SerializeField] private float waitForOrderDuration;
                                public float WaitForOrderDuration => waitForOrderDuration;
        
        [field: Header("等待料理")]
        [field: SerializeField] private float waitForReturnServingNoteDuration;
                                public float WaitForReturnServingNoteDuration => waitForReturnServingNoteDuration;
        
        [field: Header("判斷餐點是否正確")]
        [field: SerializeField] private float thinksServingNoteItemsDuration;
                                public float ThinksServingNoteItemsDuration => thinksServingNoteItemsDuration;
        
        [field: Header("表情")]
        [field: SerializeField] private float emotionDuration;
                                public float EmotionDuration => emotionDuration;
        
        #region Skin
            [field: Header("外觀")]
            [field: SerializeField] private List<CharacterSkin> SkinTypes;

            public void GetRandomSkin(SkeletonData skeletonData, out Skin skin)
            {
                skin = new Skin("newSkin");
                
                foreach (var skinType in SkinTypes)
                {
                    skinType.GetRandomSkin(out var skinName);
                    skin.AddSkin(skeletonData.FindSkin(skinName));
                }
            }
        #endregion
    }
}