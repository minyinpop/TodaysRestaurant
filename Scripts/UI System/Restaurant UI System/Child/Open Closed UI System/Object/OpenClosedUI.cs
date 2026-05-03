using System;
using Audio_System.Data;
using Audio_System.Main;
using Common.Button;
using Common.Value;
using Input_System;
using UI_System.Message_UI_System.Main;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.Object
{
    [RequireComponent(typeof(Button))]
    public sealed class OpenClosedUI : MonoBehaviour
    {
        [field: Header("按鈕")]
        [field: SerializeField] private Button button;
        
        [field: Header("物件")]
        [field: SerializeField] private GameObject open;
        [field: SerializeField] private GameObject closed;

        [field: Header("音效")]
        [field: SerializeField] private PlaySFXData clickSFX;

        private bool _isOpen;

        public Action OnOpen;
        public Action OnClosed;

        private void Awake()
        {
            button.OnClick += OnClickButton;
        }

        private void OnDestroy()
        {
            button.OnClick -= OnClickButton;
        }

        private void OnClickButton()
        {
            _isOpen = !_isOpen;

            AudioSystem.Instance.OtherSFX.PlayOneShot(clickSFX);
            
            if (_isOpen)
            {
                open.SetActive(true);
                closed.SetActive(false);
                
                OnOpen.Invoke();
            }
            else
            {
                InputSystem.DisablePlayerWalk();
                
                MessageUISystem.ShowSwitchUI(
                    content: new PopUpUIContent(
                        message: "確定要結束營業嗎？",
                        confirmButtonTitle: "結束營業",
                        cancelButtonTitle: "繼續營業",
                        closeButtonTitle: string.Empty),
                    onConfirm: () =>
                    {
                        open.SetActive(false);
                        closed.SetActive(true);
                        
                        OnClosed.Invoke();
                    },
                    onCancel: () =>
                    {
                        InputSystem.EnablePlayerWalk();
                    });
            }
        }
    }
}