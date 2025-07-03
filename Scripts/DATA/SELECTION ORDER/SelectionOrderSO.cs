using System.Collections.Generic;
using UnityEngine;

namespace DATA.SELECTION_ORDER
{
    [CreateAssetMenu(menuName = "MINYINPOP/BATTLE/SELECTION DATA", fileName = "NEW DATA")]
    internal class SelectionOrderSO : ScriptableObject
    {
        [field: SerializeField] public List<GameObject> Prefabs { get; private set; }
    }
}