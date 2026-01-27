using UI_System.Child.Message_UI_System.Object;
using UnityEngine;

namespace UI_System.Child.Message_UI_System.System
{
    internal sealed class OptionUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private RectTransform popUpUIParent;
        [field: SerializeField] private GameObject popUpUIPrefab;
        
        private PopUpUI _popUpUI;

        public void SpawnUI()
        {
            _popUpUI = Instantiate(popUpUIPrefab, popUpUIParent).GetComponent<PopUpUI>();
            
            _popUpUI.OnClickConfirmButton += OnConfirmButtonClicked;
            _popUpUI.SetButtonInteractable(true);
            return;

            void OnConfirmButtonClicked()
            {
                _popUpUI.OnClickConfirmButton -= OnConfirmButtonClicked;
                _popUpUI.SetButtonInteractable(false);
                
                Destroy(_popUpUI.gameObject);
                _popUpUI = null;
            }
        }
    }
}