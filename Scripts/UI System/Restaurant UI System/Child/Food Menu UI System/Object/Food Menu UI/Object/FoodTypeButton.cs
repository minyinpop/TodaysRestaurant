using System;
using Common.Item.Data.Food.Data.Food_Type;
using Common.Value.Type;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = Common.Button.Button;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.Object
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
            if (FoodType != FoodType.Null)
            {
                return;
            }
            
            FoodType = foodTypeData.FoodType;
            Background.color = foodTypeData.TypeColor;
            TitleTMP.text = foodTypeData.TypeName;
        }

        public void SetInteractable(bool interactable)
        {
            Button.SetInteractable(interactable);
        }
    }
}