using Data.Item.Type.Dish;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = General.Object.Button;

namespace System.Cook.CookSelection.Object
{
    [RequireComponent(typeof(Button))]
    internal sealed class StickyNote : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private Image Image;
        [field: SerializeField] private TextMeshProUGUI TMPro;

        private DishSO DishData;

        public event Action OnClick;
        
        public void Init(DishSO dishData)
        {
            DishData = dishData;
            dishData.GetItemSprite(out var dishSprite);
            Image.sprite = dishSprite;
            dishData.GetItemName(out var dishName);
            TMPro.text = dishName;
        }
    }
}