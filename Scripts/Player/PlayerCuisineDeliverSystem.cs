using System.Collections.Generic;
using Bubble.Order.Category;
using DataBase.Item.Category.Cuisine;
using DataBase.Player.Cuisine_Deliver;
using Kitchenware;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 用來管理玩家遞送料理的類。
    /// </summary>
    [RequireComponent(typeof(PlayerControlSystem))]
    public class PlayerCuisineDeliverSystem : MonoBehaviour
    {
        [Header("料理疊疊樂"), Tooltip("用於料理疊疊樂的資料庫。"), SerializeField]
        private PlayerCuisineDeliverData playerCuisineDeliverData;
         
        [Tooltip("用於顯示料理的圖片陣列。"), SerializeField]
        private List<SpriteRenderer> cuisineImageList;
        
        private void OnEnable()
        {
            KitchenwareManager.cuisineDeliver += AddCuisine;
            WaitCuisineBubble.onCuisineDeliver += Refresh;
        }

        private void OnDisable()
        {
            KitchenwareManager.cuisineDeliver -= AddCuisine;
            WaitCuisineBubble.onCuisineDeliver -= Refresh;
        }

        /// <summary>
        /// 用來添加料理，
        /// 遵循先進後出的規則。
        /// </summary>
        /// <param name="newCuisine"> 新的料理資料。 </param>
        private bool AddCuisine(Cuisine newCuisine)
        {
            // 遍歷 cuisineDataList，並判斷是否可以添加料理進陣列。
            for (var i = 0; i < playerCuisineDeliverData.cuisineDataList.Count; i++)
            {
                // 如果當前的 cuisineData 是有資料的，就直接判斷下一個資料格。
                if (playerCuisineDeliverData.cuisineDataList[i] is not null)
                    continue;
                
                cuisineImageList[i].gameObject.SetActive(true);
                cuisineImageList[i].sprite = newCuisine.Sprite;
                playerCuisineDeliverData.cuisineDataList[i] = newCuisine;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 用來更新玩家頭上的料理圖片的方法。
        /// </summary>
        private void Refresh()
        {
            for (var i = 0; i < playerCuisineDeliverData.cuisineDataList.Count; i++)
            {
                if (playerCuisineDeliverData.cuisineDataList[i] is null)
                {
                    cuisineImageList[i].sprite = null;
                    cuisineImageList[i].gameObject.SetActive(false);
                }
                else
                {
                    cuisineImageList[i].gameObject.SetActive(true);
                    cuisineImageList[i].sprite = playerCuisineDeliverData.cuisineDataList[i].Sprite;
                }
            }
        }
    }
}
