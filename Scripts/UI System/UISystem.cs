using Item.Serving_Note;
using UnityEngine;

namespace UI_System
{
    public sealed class UISystem : MonoBehaviour
    {
        [field: Header("RectTransform")]
        [field: SerializeField] private RectTransform BottomLeft;

        private void OnEnable()
        {
            ServingNoteSO.OnUseItem += InstantiateUI;
        }
        
        private void OnDisable()
        {
            ServingNoteSO.OnUseItem -= InstantiateUI;
        }
        
        private void InstantiateUI(GameObject uiObject)
        {
            Instantiate(uiObject, BottomLeft);
        }
    }
}