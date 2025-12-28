using System;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Food_Menu_UI_System
{
    public sealed class FoodMenuUISystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject uiPrefab;
        [field: SerializeField] private RectTransform uiParent;
        
        private GameObject _ui;

        public void RequiresUI(Action onComplete)
        {
            _ui = Instantiate(uiPrefab, uiParent);
            _ui.GetComponent<FoodMenu>().Show(onComplete);
        }
    }
}