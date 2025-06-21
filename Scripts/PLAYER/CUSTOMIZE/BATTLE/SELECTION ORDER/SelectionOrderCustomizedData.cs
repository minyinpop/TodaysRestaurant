using System.Collections.Generic;
using UnityEngine;

namespace PLAYER.CUSTOMIZE.BATTLE.SELECTION_ORDER
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Customize/Battle/Selection Order", fileName = "New Data")]
    internal class SelectionOrderCustomizedData : ScriptableObject
    {
        [field: SerializeField] public List<GameObject> SelectionOrderPrefabs { get; private set; }

        public void ChangeSelectionOrder(int index, GameObject obj)
        {
            SelectionOrderPrefabs[index] = obj;
        }
    }
}