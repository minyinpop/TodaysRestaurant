using UnityEngine;

namespace Kitchenware
{
    /// <summary>
    /// 用來偵測廚俱是否可以被互動的類。
    /// </summary>
    [RequireComponent(typeof(KitchenwareManager))]
    public class KitchenwareDetector : MonoBehaviour
    {
        // 自身的 KitchenwareManager 組件，用來管理廚俱的類。
        private KitchenwareManager _kitchenwareManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
            }
        }
    }
}
