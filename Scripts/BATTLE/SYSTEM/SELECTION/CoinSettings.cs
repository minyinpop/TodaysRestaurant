using BATTLE.COIN;
using UnityEngine;

namespace BATTLE.SYSTEM.SELECTION
{
    [System.Serializable]
    internal class CoinSettings
    {
        [field: Header("Parent Rect")]
        [field: SerializeField] public RectTransform SpawnParent { get; private set; }
        [field: SerializeField] public RectTransform ShowParent { get; private set; }
        
        [field: Header("Object")]
        [field: SerializeField] public GameObject CoinPrefab { get; private set; }
        public GameObject Coin { get; set; }
        public Coin CoinComponent { get; set; }
    }
}