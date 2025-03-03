using UnityEngine;

namespace Storage
{
    /// <summary>
    /// 用來控制儲物介面的類。
    /// </summary>
    public class StorageUI : MonoBehaviour
    {
        [Header("資料庫"), Tooltip("用來當作儲物介面的資料庫。"), SerializeField]
        private StorageData storageData;
    }
}
