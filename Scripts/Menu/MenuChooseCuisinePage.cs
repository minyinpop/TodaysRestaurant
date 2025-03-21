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
        
        private void InitSlot(PlayerChooseCuisineData chooseData)
        {
            
        }

        private void AddSlotToList(PlayerChooseCuisineData chooseData)
        {
            foreach (var data in chooseData.CuisineList)
            {
                var slot = Instantiate(slotPrefab, spawnPoint);
                // TODO: slot.GetComponent<MenuChooseCuisineSlot>().Refresh(data);
                _slotList.Add(slot);
            }
        }
    }
}
