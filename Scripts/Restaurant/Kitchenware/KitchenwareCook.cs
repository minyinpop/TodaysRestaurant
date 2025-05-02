using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareCook : MonoBehaviour
    {
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        [field: Header("氣泡的預製件")]
        [field: SerializeField] private GameObject EmptyBubblePrefab { get; set; }
        
        private GameObject CurrentBubble { get; set; }

        private void Start()
        {
            CurrentBubble = Instantiate(EmptyBubblePrefab, BubbleParent);
        }
    }
}