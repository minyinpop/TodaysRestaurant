using Storage;
using UnityEngine;

namespace Player
{
    public class PlayerBagSystem : MonoBehaviour
    {
        [field: Header("資料"), Tooltip("玩家背包的資料庫組件"), SerializeField]
        public StorageDataSO StorageDataSO { get; private set; }
    }
}