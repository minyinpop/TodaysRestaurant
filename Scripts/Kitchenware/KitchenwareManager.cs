using UnityEngine;

namespace Kitchenware
{
    /// <summary>
    /// 用來管理廚具的類。
    /// 需要 KitchenwareDetector 與 KitchenwareBubble 這兩個類的支援。
    /// </summary>
    [RequireComponent(typeof(KitchenwareDetector))]
    [RequireComponent(typeof(KitchenwareBubble))]
    public class KitchenwareManager : MonoBehaviour
    {
        // 自身的 KitchenwareDetector 組件，用來檢測玩家是否進入偵測空間的類。
        private KitchenwareDetector _kitchenwareDetector;
        
        // 自身的 KitchenwareBubble 組件，用來管理廚俱的氣泡的類。
        private KitchenwareBubble _kitchenwareBubble;

        private void Awake()
        {
            _kitchenwareDetector = GetComponent<KitchenwareDetector>();
        }
    }
}
