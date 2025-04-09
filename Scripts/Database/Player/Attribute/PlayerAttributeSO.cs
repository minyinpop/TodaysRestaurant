using System;
using UnityEngine;

namespace Database.Player.Attribute
{
    // ==================================================
    // 玩家屬性的資料庫。
    // 裡面的資料開放更改，請注意使用。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Player Attribute", menuName = "Minyinpop/Player/Attribute", order = 1)]
    public class PlayerAttributeSO : ScriptableObject
    {
        // ========== { 判斷相關 } ==========
        
        [field: Header("執行設定"), Tooltip("走路判斷。"), SerializeField]
        public CanThenRunning Walk { get; set; }
        
        [field: Tooltip("跑步判斷。"), SerializeField]
        public CanThenRunning Run { get; set; }
        
        
        
        // ========== { 移動相關 } ==========
        
        [field: Header("移動設定"), Tooltip("- 基本的移動速度。\n- 使用物理引擎來移動。"), SerializeField]
        public float BasicMoveSpeed { get; set; }

        [field: Tooltip("- 跑步時的移速加成。\n- 使用物理引擎來移動。"), SerializeField]
        public float RunSpeedMultiplier { get; set; }
        
        
        
        // ========== { 動畫相關 } ==========
        
        [field: Header("動畫設定"), Tooltip("- 播放玩家眨眼動畫的閥值。\n- 使用秒數來計算。"), SerializeField]
        public Range EyeBlinkRange { get; set; }
    }
    
    
    
    // ==================================================
    // 用來包裝是否可以執行且是否正在執行的參數。
    // ==================================================
    [Serializable]
    public struct CanThenRunning
    {
        [field: Tooltip("參數是否可以執行？"), SerializeField]
        public bool Can { get; set; }

        [field: Tooltip("參數是否正在執行？"), SerializeField]
        public bool IsRunning { get; set; }
    }
    
    
    
    // ==================================================
    // 用來包裝有最大值與最小值的參數。
    // ==================================================
    [Serializable]
    public struct Range
    {
        [field: Tooltip("參數的最大值。"), SerializeField]
        public float Max { get; set; }
        
        [field: Tooltip("參數的最小值。"), SerializeField]
        public float Min { get; set; }
    }
}