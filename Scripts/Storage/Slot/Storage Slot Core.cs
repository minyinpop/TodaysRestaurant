using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot
{
    public abstract class StorageSlotCore : MonoBehaviour
    {
        [field: Header("組件"), Tooltip("物品的圖片組件"), SerializeField]
        private Image itemImage;

        [field: Tooltip("數量的文字組件"), SerializeField]
        private TextMeshProUGUI itemAmountTMP;
    }
}
