using DataBase.Customer.Attribute;
using DataBase.Route;
using UnityEngine;

namespace NPC.Customer
{
    /// <summary>
    /// 用來管理顧客移動的類。
    /// 需要 CustomerManager 這個類的支援。
    /// </summary>
    [RequireComponent(typeof(CustomerManager))]
    public class CustomerMove : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("用於讀取角色的屬性的資料庫。"), SerializeField]
        private CustomerAttributeData attributeData;
        
        [Tooltip("用於顧客移動的路縣的座標點的資料庫。"), SerializeField]
        private RouteData routeData;
        
        // 當前的路徑的索引的暫存。
        private int _nowSelectRouteIndex;
        
        // 自身的 CustomerManager 的類，用於顧客的管理得類。
        private CustomerManager _customerManager;

        private void Awake()
        {
            _customerManager = GetComponent<CustomerManager>();
        }

        private void FixedUpdate()
        {
            switch (_customerManager.CustomerState)
            {
                case CustomerManager.State.GoToSeat:
                {
                    var current = transform.position;
                    var target = routeData.RouteAToBList[_nowSelectRouteIndex];
                    var maxDistanceDelta = attributeData.MoveSpeed * Time.fixedDeltaTime;

                    transform.position = Vector3.MoveTowards(current, target, maxDistanceDelta);

                    // 如果現在位置接近目標位置，就進入判斷。
                    if (Vector3.Distance(transform.position, routeData.RouteAToBList[_nowSelectRouteIndex]) > .01f)
                        return;

                    _nowSelectRouteIndex++;

                    // 如果當前的目標位置是最後一個，那就重製 _nowSelectRouteIndex，並更換顧客狀態。
                    if (_nowSelectRouteIndex == routeData.RouteAToBList.Count)
                    {
                        _nowSelectRouteIndex = 0;

                        transform.position = routeData.TargetSeatPosition;
                        _customerManager.OnSeat();
                        
                        return;
                    }

                    return;
                }
                case CustomerManager.State.Leaving:
                {
                    break;
                }
            }
        }
    }
}
