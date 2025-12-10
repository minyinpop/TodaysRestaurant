using Data.Food.Food_Type.Base;
using Data.General.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = Common.Button;

namespace System.Economy.Child.Food_Menu.Object
{
    [RequireComponent(typeof(Button))]
    internal sealed class FoodTypeButton : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Image Background;
        [field: SerializeField] private TextMeshProUGUI TitleTMP;
        [field: SerializeField] private Button Button;

        private FoodType FoodType = FoodType.Null;

        private void OnEnable()
        {
            Button.OnClick += OnClicked;
        }
        
        private void OnDisable()
        {
            Button.OnClick -= OnClicked;
        }

        public event Action<FoodType> OnClick;
        private void OnClicked() { OnClick?.Invoke(FoodType); }
        
        public void Init(FoodTypeSO foodTypeData)
        {
            if (FoodType != FoodType.Null) return;
            foodTypeData.GetValues(out var foodType, out var typeName, out var typeColor);
            FoodType = foodType;
            Background.color = typeColor;
            TitleTMP.text = typeName;
        }

        public void SetInteractable(bool interactable)
        {
            Button.SetInteractable(interactable);
        }
    }
}