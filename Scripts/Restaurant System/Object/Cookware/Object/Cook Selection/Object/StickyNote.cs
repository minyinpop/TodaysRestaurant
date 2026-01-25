using System;
using Item.Food;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = Common.Object.Button;

namespace Restaurant_System.Object.Cookware.Object.Cook_Selection.Object
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
            Button.OnClicked += OnStickyNoteClicked;
        }
        
        private void OnDisable()
        {
            Button.OnClicked -= OnStickyNoteClicked;
        }

        public void Init(FoodSO foodData)
        {
            FoodData = foodData;
            Image.sprite = foodData.ItemSprite;
            TMPro.text = foodData.ItemName;
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