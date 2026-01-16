using System;
using Input_System.Main;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Main;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Food_Menu_UI_System
{
    public sealed class FoodMenuUISystem : MonoBehaviour, IUISystem
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject uiPrefab;
        [field: SerializeField] private RectTransform uiParent;
        
        private GameObject _ui;

        public void SpawnUI(Action onClose)
        {
            _ui = Instantiate(uiPrefab, uiParent);
            _ui.GetComponent<FoodMenu>().Initialize(onClose);
        }

        public void DestroyUI()
        {
            Destroy(_ui);
            _ui = null;
        }
    }
}