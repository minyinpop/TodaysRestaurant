using UI_System.Lobby_UI_System.Child.Map_UI_System.Object;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Map_UI_System.System
{
    public sealed class MapUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private MapUI mapUI;

        private void Awake()
        {
            if (mapUI == null)
            {
                Debug.Log($"{nameof(MapUISystem)} > {nameof(mapUI)} cannot be null.");
            }
        }

        public void RequireUI()
        {
            mapUI.gameObject.SetActive(!mapUI.gameObject.activeSelf);
        }
    }
}