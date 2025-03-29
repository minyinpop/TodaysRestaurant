using UnityEngine;

namespace Kitchenware
{
    /// <summary>
    /// 用來偵測廚俱是否可以被互動的類。
    /// </summary>
    [RequireComponent(typeof(KitchenwareManager))]
    public class KitchenwareDetectorSystem : MonoBehaviour
    {
        // 自身的 KitchenwareBubble 組件，用來管理廚俱的類。
        private KitchenwareManager _kitchenwareManager;

        private void Awake()
        {
            _kitchenwareManager = GetComponent<KitchenwareManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _kitchenwareManager.OnPlayerNearby(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _kitchenwareManager.OnPlayerNearby(false);
        }
    }
}
