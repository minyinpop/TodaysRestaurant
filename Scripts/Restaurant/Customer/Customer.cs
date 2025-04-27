using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(CustomerNavigation))]
    [RequireComponent(typeof(CustomerAnimator))]
    public class Customer : MonoBehaviour
    {
        private StateEnum State { get; set; } = StateEnum.GoInside;
        private enum StateEnum
        {
            GoInside,
            OnSeat,
            GoOutside
        }

        // 僅供 GameObject 本身的 CustomerNavigation 呼叫。
        public void OnTargetPoint()
        {
            Debug.Log("到位置了");
        }
    }
}