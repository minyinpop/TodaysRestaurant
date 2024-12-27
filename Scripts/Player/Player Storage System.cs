using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Player
{
    public class PlayerStorageSystem : MonoBehaviour
    {
        // 總物品格
        [field: SerializeField] private List<GridCore> totalGrids;
    }
}