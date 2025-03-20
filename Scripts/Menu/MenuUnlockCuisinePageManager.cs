using System.Collections.Generic;
using Player.Menu.UnlockCuisine;
using UnityEngine;

namespace Menu
{
    /// <summary>
    /// 用於管理菜單頁面的左半邊，專門顯示玩家當前的類別，有解鎖甚麼菜品。
    /// 與 MenuManager 這個類為綁定狀態。
    /// </summary>
    public class MenuUnlockCuisinePageManager : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("玩家解鎖的主菜的資料庫。"), SerializeField]
        private PlayerUnlockCuisineData mainCourseData;
        
        [Tooltip("玩家解鎖的飲料的資料庫。"), SerializeField]
        private PlayerUnlockCuisineData drinkData;
        
        [Header("已解鎖的菜品"), Tooltip("用於顯示已解鎖的蔡品的預製件。"), SerializeField]
        private GameObject slotPrefab;

        [Tooltip("以解鎖的蔡品的儲物格的生成位置。"), SerializeField]
        private RectTransform spawnPoint;
        
        // 當前選擇的菜品的類別。
        private ChooseType _chooseType;
        private enum ChooseType
        {
            MainCourse,
            Drink
        }
        
        // 當前生成的格子的暫存列表。
        private readonly List<GameObject> _slotList = new();

        private void OnEnable()
        {
            InitUnlockCuisineSlot();
        }

        /// <summary>
        /// 依照 _chooseType 當前選擇的菜品類別，來生成相對應並且已解鎖的菜品。
        /// </summary>
        private void InitUnlockCuisineSlot()
        {
            ClearSlotList();
            
            switch (_chooseType)
            {
                case ChooseType.MainCourse:
                {
                    InitSlotList(mainCourseData);
                    break;
                }
                case ChooseType.Drink:
                {
                    InitSlotList(drinkData);
                    break;
                }
            }
        }

        /// <summary>
        /// 依照已解鎖的菜品來生成格子。
        /// </summary>
        /// <param name="cuisineData"> 傳入的已解鎖的菜品的資料。 </param>
        private void InitSlotList(PlayerUnlockCuisineData cuisineData)
        {
            foreach (var data in cuisineData.CuisineList)
            {
                var slot = Instantiate(slotPrefab, spawnPoint);
                slot.GetComponent<MenuUnlockCuisineSlot>().Refresh(data);
                _slotList.Add(slot);
            }
        }

        /// <summary>
        /// 清除當前菜品所生成的格子與暫存資料。
        /// </summary>
        private void ClearSlotList()
        {
            foreach (var slot in _slotList)
            {
                Destroy(slot);
            }

            _slotList.Clear();
        }

        /// <summary>
        /// 當主菜選單按鈕按下後，就把當前選擇菜品的類別，切換成主菜。
        /// </summary>
        public void OnMainCourseTypeButtonClick()
        {
            _chooseType = ChooseType.MainCourse;
            InitUnlockCuisineSlot();
        }
        
        /// <summary>
        /// 當飲品選單按鈕按下後，就把當前選擇菜品的類別，切換成飲品。
        /// </summary>
        public void OnDrinkTypeButtonClick()
        {
            _chooseType = ChooseType.Drink;
            InitUnlockCuisineSlot();
        }
    }
}
