using Data.Item.Type.Dish;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = General.Object.Button;

namespace System.Cook.Selection_UI.Object
{
    [RequireComponent(typeof(Button))]
    internal sealed class StickyNote : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Object")]
        [field: SerializeField] private Image Image;
        [field: SerializeField] private TextMeshProUGUI TMPro;

        private DishSO DishData;

        public event Action<DishSO> OnClick;

        private void OnEnable()
        {
            Button.OnClick += OnStickyNoteClicked;
        }
        
        private void OnDisable()
        {
            Button.OnClick -= OnStickyNoteClicked;
        }

        public void Init(DishSO dishData)
        {
            DishData = dishData;
            dishData.GetItemSprite(out var dishSprite);
            Image.sprite = dishSprite;
            dishData.GetItemName(out var dishName);
            TMPro.text = dishName;
        }

        public void SetInteractable(bool interactable)
        {
            Button.SetInteractable(interactable);
        }

        private void OnStickyNoteClicked()
        {
            OnClick?.Invoke(DishData);
        }
    }
}