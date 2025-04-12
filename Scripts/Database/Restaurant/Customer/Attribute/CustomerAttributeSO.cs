using System;
using UnityEngine;

namespace Database.Restaurant.Customer.Attribute
{
    // ==================================================
    // 顧客的屬性資料。
    // 裡面的資料開放更改，請注意使用。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Customer Path", menuName = "Minyinpop/Restaurant/Customer/Attribute", order = 1)]
    public class CustomerAttributeSO : ScriptableObject
    { 
        // ========== { 移動相關 } ==========
        
        [field: Header("移動設定"), Tooltip("- 基本的移動速度。\n- 使用物理引擎來移動。"), SerializeField]
        public float BasicMoveSpeed { get; private set; }
        
        
        
        // ========== { 動畫相關 } ==========
        
        [field: Header("動畫設定"), Tooltip("- 播放玩家眨眼動畫的閥值。\n- 使用秒數來計算。"), SerializeField]
        public Range EyeBlinkRange { get; private set; }
        
        // ========== { 氣泡相關 } ==========
        
        [field: Header("氣泡時長設定"), Tooltip("- 顧客思考要點甚麼餐點的時間。\n- 使用秒數來計算。"), SerializeField]
        public Range ThinkingTime { get; private set; }
        
        [field: Tooltip("- 顧客在等待點餐食的耐心閥值。\n- 使用秒數來計算。"), SerializeField]
        public Range OrderingPatienceTime { get; private set; }
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