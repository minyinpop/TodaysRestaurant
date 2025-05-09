using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareGame : MonoBehaviour
    {
        [field: Header("小遊戲的生成位置")]
        [field: SerializeField] private Transform GameParent { get; set; }
        
        [field: Header("小遊戲的預製件")]
        [field: SerializeField] private GameObject GamePrefab { get; set; }
        private GameObject GameObj { get; set; }
    }
}