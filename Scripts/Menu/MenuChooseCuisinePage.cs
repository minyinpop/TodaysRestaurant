using System.Collections.Generic;
using Menu.Slot;
using Player.Menu.ChooseCuisine;
using UnityEngine;

namespace Menu
{
    /// <summary>
    /// 用於管理菜單頁面的右半邊，專門顯示玩家當前上架了甚麼菜品。
    /// 與 MenuManager 這個類為綁定狀態。
    /// </summary>
    public class MenuChooseCuisinePage : MonoBehaviour
    {
        [Header("已解鎖的菜品"), Tooltip("用於顯示已解鎖的蔡品的預製件。"), SerializeField]
        private GameObject slotPrefab;

        [Tooltip("以解鎖的蔡品的儲物格的生成位置。"), SerializeField]
        private RectTransform spawnPoint;
        
        // 當前生成的格子的暫存列表，用於 UI。
        private readonly List<GameObject> _slotList = new();
        
        /// <summary>
        /// 初始化已選擇的菜品的格子的顯示，用於 UI。
        /// </summary>
        /// <param name="chooseData"> 傳入的已解鎖的菜品的資料。 </param>
        public void InitSlot(PlayerChooseCuisineData chooseData)
        {
            ClearSlotList();
            AddSlotToList(chooseData);
        }

        /// <summary>
        /// 依照已解鎖的菜品來生成格子。
        /// </summary>
        /// <param name="chooseData"> 傳入的已解鎖的菜品的資料。 </param>
        private void AddSlotToList(PlayerChooseCuisineData chooseData)
        {
            foreach (var slotData in chooseData.SlotDataList)
            {
                var slot = Instantiate(slotPrefab, spawnPoint);
                slot.GetComponent<MenuChooseCuisineSlot>().Refresh(slotData);
                _slotList.Add(slot);
            }
        }
        
        /// <summary>
        /// 清除當前菜品所生成的格子與暫存資料。
        /// </summary>
        private void ClearSlotList()
        {
            foreach (var slot in _slotList)
                Destroy(slot);

            _slotList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slotIndex"></param>
        /// <param name="newSlotData"></param>
        public void AddDataToList(int slotIndex, PlayerChooseCuisineSlotData newSlotData)
        {
            _slotList[slotIndex].GetComponent<MenuChooseCuisineSlot>().Refresh(newSlotData);
        }
    }
}
