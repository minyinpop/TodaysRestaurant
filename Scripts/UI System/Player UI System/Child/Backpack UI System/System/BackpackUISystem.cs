using UI_System.Player_UI_System.Child.Backpack_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Backpack_UI_System.System
{
    public sealed class BackpackUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private BackpackUI backpackUI;

        private void Awake()
        {
            if (backpackUI == null)
            {
                Debug.Log($"{nameof(BackpackUISystem)} > {nameof(backpackUI)} cannot be null.)");
            }
        }

        public void RequireBackpackUI()
        {
            backpackUI.gameObject.SetActive(!backpackUI.gameObject.activeSelf);
            mask.SetActive(backpackUI.gameObject.activeSelf);
        }
    }
}