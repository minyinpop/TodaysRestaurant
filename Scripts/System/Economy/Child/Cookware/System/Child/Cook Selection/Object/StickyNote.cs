using Data.Item.Type.Food;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace System.Economy.Child.Cookware.System.Child.Cook_Selection.Object
{
    [RequireComponent(typeof(Button))]
    internal sealed class StickyNote : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Object")]
        [field: SerializeField] private Image Image;
        [field: SerializeField] private TextMeshProUGUI TMPro;

        private FoodSO FoodData;

        public event Action<FoodSO> OnClick;

        private void OnEnable()
        {
            Button.OnClick += OnStickyNoteClicked;
        }
        
        private void OnDisable()
        {
            Button.OnClick -= OnStickyNoteClicked;
        }

        public void Init(FoodSO foodData)
        {
            FoodData = foodData;
            foodData.GetItemSprite(out var dishSprite);
            Image.sprite = dishSprite;
            foodData.GetItemName(out var dishName);
            TMPro.text = dishName;
        }

        public void SetInteractable(bool interactable)
        {
            Button.SetInteractable(interactable);
        }

        private void OnStickyNoteClicked()
        {
            OnClick?.Invoke(FoodData);
        }
    }
}