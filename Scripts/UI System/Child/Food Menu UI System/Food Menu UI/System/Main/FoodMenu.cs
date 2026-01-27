using System;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Child;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Child.Open_Page.Main;
using UnityEngine;

namespace UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Main
{
    internal sealed class FoodMenu : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private OpenState openState;
        [field: SerializeField] private CloseState closeState;

        public event Action OnConfirm;

        private void Awake()
        {
            openState.OnConfirm += OnOpenStateConfirmed;
            closeState.OnConfirm += OnCloseStateConfirmed;
        }

        private void OnDestroy()
        {
            openState.OnConfirm -= OnOpenStateConfirmed;
            closeState.OnConfirm -= OnCloseStateConfirmed;
        }

        private void OnOpenStateConfirmed()
        {
            if (OnConfirm == null)
            {
                Debug.LogWarning($"{gameObject.name} > FoodMenu > OnConfirm cannot be null.");
                gameObject.SetActive(false);
                return;
            }

            OnConfirm.Invoke();
        }

        private void OnCloseStateConfirmed()
        {
            closeState.Hide();
            openState.Show();
        }
    }
}