using UnityEngine;

namespace System.Cook.CookSelection.Main
{
    internal sealed class CookSelectionSystem : MonoBehaviour
    {
        [field: SerializeField] private GameObject UIObject;

        private void OnEnable()
        {
            Cookware.Cookware.OnClickEmptyBubble += Open;
        }

        private void OnDisable()
        {
            Cookware.Cookware.OnClickEmptyBubble -= Open;
        }

        private void Open()
        {
            Debug.Log("Open");
        }
    }
}