using System.Collections;
using DataBase.Item.Category.Cuisine;
using UnityEngine;

namespace Kitchenware
{
    /// <summary>
    /// 用於執行廚俱烹飪的類。
    /// </summary>
    [RequireComponent(typeof(KitchenwareManager))]
    public class KitchenwareCookSystem : MonoBehaviour
    {
        // 自身的 KitchenwareBubble 組件，用來管理廚俱的類。
        private KitchenwareManager _kitchenwareManager;

        // 用於暫存烹飪的異步協程。
        private IEnumerator _cookProcess;
        
        // 用於暫存被烹飪的料理的資料。
        private Cuisine _cuisineData;

        private void Awake()
        {
            _kitchenwareManager = GetComponent<KitchenwareManager>();
        }

        private void OnDisable()
        {
            if (_cookProcess is not null)
            {
                StopCoroutine(_cookProcess);
                _cookProcess = null;
            }
        }

        /// <summary>
        /// 用於開始烹飪協程的方法。
        /// </summary>
        public void StartCookProcess(Cuisine newCuisineData)
        {
            _cuisineData = newCuisineData;
            
            _cookProcess = CookProcess();
            StartCoroutine(_cookProcess);
        }

        /// <summary>
        /// 烹飪的異步協程的方法。
        /// </summary>
        /// <returns> 回傳協程的資料道系統。 </returns>
        private IEnumerator CookProcess()
        {
            // TODO: 繼續製作烹飪的邏輯。
            
            yield return null;
        }
    }
}
