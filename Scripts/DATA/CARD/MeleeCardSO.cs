using UnityEngine;

namespace DATA.CARD
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Battle/Attack Card Data", fileName = "New Data")]
    internal class MeleeCardSO : ScriptableObject
    {
        [field: SerializeField] public Information Information { get; private set; }
        [field: SerializeField] public Attribute Attribute { get; private set; }
    }
    
    [System.Serializable]
    internal class Information
    {
        /// <summary>
        /// 卡片在資料庫裡的編號
        /// </summary>
        [field: Tooltip("卡片在資料庫裡的編號")]
        [field: SerializeField] public int ID { get; private set; }
        
        
        
        /// <summary>
        /// 卡片的正面圖片
        /// </summary>
        [field: Tooltip("卡片的正面圖片")]
        [field: SerializeField] public Sprite FImg { get; private set; }
        /// <summary>
        /// 卡片的背面圖片
        /// </summary>
        [field: Tooltip("卡片的背面圖片")]
        [field: SerializeField] public Sprite BImg { get; private set; }
        
        
        
        /// <summary>
        /// 卡片名稱
        /// </summary>
        [field: Tooltip("卡片名稱")]
        [field: SerializeField] public string Name { get; private set; }
        /// <summary>
        /// 卡片名稱的顏色
        /// </summary>
        [field: Tooltip("卡片名稱的顏色")]
        [field: SerializeField] public Color NameColor { get; private set; }
        
        
        
        /// <summary>
        /// 卡片的詳細描述
        /// </summary>
        [field: Tooltip("卡片的詳細描述")]
        [field: TextArea]
        [field: SerializeField] public string Description { get; private set; }
    }

    [System.Serializable]
    internal class Attribute
    {
        /// <summary>
        /// 有護甲目標時的傷害
        /// </summary>
        [field: Tooltip("有護甲目標時的傷害")]
        [field: SerializeField] public int ADmg { get; private set; }
        /// <summary>
        /// 無護甲目標時的傷害
        /// </summary>
        [field: Tooltip("無護甲目標時的傷害")]
        [field: SerializeField] public int BDmg { get; private set; }
        
        
        
        /// <summary>
        /// 此卡片的總爆擊率
        /// </summary>
        [field: Tooltip("此卡片的總爆擊率")]
        [field: Range(0, 100)]
        [field: SerializeField] public int CritRate { get; private set; }
        /// <summary>
        /// 對護甲攻擊時的爆擊傷害乘數
        /// </summary>
        [field: Tooltip("對護甲攻擊時的爆擊傷害乘數")]
        [field: SerializeField] public float CritMult_ADmg { get; private set; }
        /// <summary>
        /// 對無護甲攻擊時的爆擊傷害乘數
        /// </summary>
        [field: Tooltip("對無護甲攻擊時的爆擊傷害乘數")]
        [field: SerializeField] public float CritMult_BDmg { get; private set; }
    }
}