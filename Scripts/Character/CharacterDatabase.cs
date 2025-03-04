using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    /// <summary>
    /// 用來整合所有角色的各種資料以及參數等等。
    /// </summary>
    [CreateAssetMenu(fileName = "New Character Database", menuName = "Character Data/Database", order = 1)]
    public class CharacterDatabase : ScriptableObject
    {
        [field: Header("背包設定"), Tooltip("背包當前的等級，用於確認要解鎖多少背包的儲物格。"), SerializeField]
        public int BagLevel { get; private set; }
        
        [field: Tooltip("每個背包等級所解鎖的背包格子數量，用於確認背包的儲物格的數量"), SerializeField]
        public List<int> BagStorageSlotQuantityPerLevel { get; private set; }
    }
}
