using System.Collections;
using Bubble.Kitchenware;
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
        
        // 自身的 KitchenwareBubbleSystem 組件，用來管理廚俱的氣泡的類。
        private KitchenwareBubbleSystem _kitchenwareBubbleSystem;

        
        
        // 用於暫存烹飪的異步協程。
        private IEnumerator _cookProcess;
        
        // 用於暫存被烹飪的料理的資料。
        private Cuisine _cuisineData;
        
        // 剩餘的烹飪時間的暫存。
        private float _cookTimeRemaining;

        private void Awake()
        {
            _kitchenwareManager = GetComponent<KitchenwareManager>();
            _kitchenwareBubbleSystem = GetComponent<KitchenwareBubbleSystem>();
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
        public void StartCook(Cuisine newCuisineData)
        {
            _cuisineData = newCuisineData;
            _cookTimeRemaining = newCuisineData.CookTime;
            _kitchenwareManager.CuisineData = newCuisineData;
            
            _cookProcess = StartCookProcess();
            StartCoroutine(_cookProcess);
        }

        private IEnumerator StartCookProcess()
        {
            _kitchenwareBubbleSystem.InitCookBubble();
            
            var maxTime = Mathf.Ceil(_cuisineData.CookTime / 2);
            var minTime = Mathf.Floor(_cuisineData.CookTime / 3);
            var selectGameTime = Random.Range(minTime, maxTime);

            _cookTimeRemaining -= selectGameTime;

            yield return new WaitForSeconds(selectGameTime);
            
            _kitchenwareBubbleSystem.Bubble.GetComponent<KitchenwareBubbleBase>().IsGameCanPlay(true);
        }

        /// <summary>
        /// 用於繼續烹飪協成的方法。
        /// </summary>
        public void ContinueCook()
        {
            _cookProcess = ContinueCookProcess();
            StartCoroutine(_cookProcess);
        }

        private IEnumerator ContinueCookProcess()
        {
            yield return new WaitForSeconds(_cookTimeRemaining);
            _kitchenwareBubbleSystem.InitDoneBubble();
        }
    }
}
