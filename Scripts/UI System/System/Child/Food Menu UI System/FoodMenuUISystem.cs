using System;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Main;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Food_Menu_UI_System
{
    public sealed class FoodMenuUISystem : MonoBehaviour, IUISystem
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject uiPrefab;
        [field: SerializeField] private RectTransform uiParent;
        
        [field: Header("Mask")]
        [field: SerializeField] private GameObject maskImage;
        
        private GameObject _ui;

        public void SpawnUI(Action onClose)
        {
            maskImage.SetActive(true);
            
            _ui = Instantiate(uiPrefab, uiParent);
            _ui.GetComponent<FoodMenu>().Initialize(onClose);
        }

        public void DestroyUI()
        {
            Destroy(_ui);
            _ui = null;
            
            maskImage.SetActive(false);
        }
    }
}