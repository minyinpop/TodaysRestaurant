using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Backpack_UI_System
{
    public sealed class BackpackUISystem : MonoBehaviour, IUISystem
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private GameObject uiPrefab;
        [field: SerializeField] private RectTransform uiParent;
        
        private GameObject _ui;

        public void PerformBackpack()
        {
            if (_ui is null)
            {
                _ui = Instantiate(uiPrefab, uiParent);
                _ui.SetActive(true);
            }
            else
            {
                _ui.SetActive(!_ui.activeSelf);
            }
            
            mask.SetActive(_ui.activeSelf);
        }
    }
}