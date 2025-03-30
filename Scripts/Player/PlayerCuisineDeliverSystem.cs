using System.Collections.Generic;
using DataBase.Item.Category.Cuisine;
using Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    /// 用來管理玩家運送料理的類。
    /// </summary>
    [RequireComponent(typeof(PlayerControlSystem))]
    public class PlayerCuisineDeliverSystem : MonoBehaviour
    {
        // [Header("組件"), Tooltip(""), SerializeField]
        // private Image 
        // 用來暫存頭上的料理的陣列。
        private List<Cuisine> _deliveredCuisineList = new List<Cuisine>();
        
        private void OnEnable()
        {
            KitchenwareManager.cuisineDeliver += AddCuisine;
        }

        private void OnDisable()
        {
            KitchenwareManager.cuisineDeliver -= AddCuisine;
        }

        /// <summary>
        /// 用來添加料理，
        /// 遵循先進後出的規則。
        /// </summary>
        /// <param name="newCuisine"> 新料理的資料。 </param>
        private void AddCuisine(Cuisine newCuisine)
        {
            _deliveredCuisineList.Add(newCuisine);
        }
    }
}
