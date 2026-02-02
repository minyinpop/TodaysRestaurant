using Common.Value;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI_System.Lobby_UI_System.Child.Map_UI_System.Object
{
    public sealed class MapUI : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [field: Header("Objects")]
        [field: SerializeField] private Camera mapCamera;
    
        [field: Header("Settings")]
        [field: SerializeField] private Range positionXRange;

        private Vector3 _lastPointerPosition;

        private void Awake()
        {
            if (mapCamera == null)
            {
                Debug.Log($"{nameof(MapUI)} > {nameof(mapCamera)} cannot be null.");
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastPointerPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            positionXRange.GetValues(out var min, out var max);
            var targetX = eventData.position.x - _lastPointerPosition.x;

            var move = new Vector3(
                x: Mathf.Clamp(mapCamera.transform.position.x + -targetX, min, max),
                y: mapCamera.transform.position.y,
                z: mapCamera.transform.position.z
            );

            mapCamera.transform.position = move;
            _lastPointerPosition = eventData.position;
        }
    }
}