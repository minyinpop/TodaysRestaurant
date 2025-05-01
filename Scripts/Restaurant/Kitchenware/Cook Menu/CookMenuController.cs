using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Cook_Menu
{
    public class CookMenuController : MonoBehaviour
    {
        [field: Header("便利貼的預製件")]
        [field: SerializeField] private List<GameObject> StickyNotes { get; set; }
        
        [field: Header("自身組件")]
        [field: SerializeField] private Button CraftButton { get; set; }
        [field: SerializeField] private Button CloseButton { get; set; }

        public void Init()
        {
            Debug.Log("A");
        }
    }
}