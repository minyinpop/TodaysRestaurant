using System;
using Common.Object;
using TMPro;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Pick_UI.Child
{
    [RequireComponent(typeof(Button))]
    public sealed class LevelPickButton : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Button button;

        public event Action OnClick;

        private void Awake()
        {
            button.OnClick += OnClick;
        }

        private void OnDestroy()
        {
            button.OnClick -= OnClick;
        }

        public void SetTitle(string title)
        {
            button.SetTitle(title);
        }
    }
}