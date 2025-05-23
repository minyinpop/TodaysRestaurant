using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Player.Dish_Deliver;
using Restaurant.Customer;
using Restaurant.Kitchenware;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Player
{
    internal class PlayerDishDeliver : MonoBehaviour
    {
        [field: Header("在運送料理的資料")]
        [field: SerializeField] private DishDeliverSO DishDeliver { get; set; }
        
        [field: Header("料理顯示的圖片組件")]
        [field: SerializeField] private List<Image> DishImages { get; set; }

        private void OnEnable()
        {
            KitchenwareManager.GetDishEvent += AddDish;
            CustomerOrder.TryTakeDishEvent += TryTakeDish;
        }

        private void OnDestroy()
        {
            KitchenwareManager.GetDishEvent -= AddDish;
            CustomerOrder.TryTakeDishEvent -= TryTakeDish;

            DishDeliver.Clear();
        }

        private bool AddDish(DishSO dish)
        {
            return DishDeliver.AddDish(DishImages, dish);
        }

        private DishSO TryTakeDish()
        {
            var takeDish = DishDeliver.TryTakeDish();

            if (takeDish is null)
                return null;

            foreach (var dishImage in DishImages)
            {
                dishImage.sprite = null;
                dishImage.gameObject.SetActive(false);
            }
            
            for (var i = DishImages.Count - 1; i >= 0; i--)
            {
                if (DishDeliver.CheckDishExist(i))
                {
                    DishImages[i].gameObject.SetActive(true);
                    DishImages[i].sprite = DishDeliver.Dishes[i].Sprite;
                    continue;
                }
                
                DishImages[i].sprite = null;
                DishImages[i].gameObject.SetActive(false);
                return takeDish;
            }
            
            return null;
        }
    }
}