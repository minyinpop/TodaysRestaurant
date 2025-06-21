using System.Collections.Generic;
using UnityEngine;

namespace PLAYER.BATTLE.ACTIVE_CARD
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Battle/Active Card", fileName = "New Data")]
    internal class ActiveCardData : ScriptableObject
    {
        [field: SerializeField] public List<GameObject> ActiveCardPrefabs { get; private set; }
    }
}