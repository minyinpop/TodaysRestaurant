using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Restaurant.Mini_Game.Stockpot
{
    public class DragManager : MonoBehaviour
    {
        [field: Header("可以被拖曳的物件標籤")]
        [field: SerializeField] private string DraggableObjectTag { get; set; }
        
        private InputManager Input { get; set; }
        private Vector2 MousePos => Input.Mouse.MousePos.ReadValue<Vector2>();
        
        private Camera MainCamera { get; set; }
        
        private GameObject DraggedObject { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }

        private void Awake()
        {
            Input = InputSystem.Input;
            MainCamera = Camera.main;
        }

        private void OnEnable()
        {
            Input.Mouse.LeftClick.started += StartDetect;
            Input.Mouse.LeftClick.canceled += StopDetect;
        }

        private void OnDisable()
        {
            Input.Mouse.LeftClick.started -= StartDetect;
            Input.Mouse.LeftClick.canceled -= StopDetect;
            
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }

        private void StartDetect(InputAction.CallbackContext context)
        {
            var plane = new Plane(Vector3.forward, Vector3.zero);
            var ray = MainCamera.ScreenPointToRay(MousePos);

            if (!plane.Raycast(ray, out var enter))
                return;
            
            var hitPoint = ray.GetPoint(enter);
            var hit = Physics2D.Raycast(hitPoint, Vector2.zero, Mathf.Infinity, LayerMask.GetMask(DraggableObjectTag));

            if (hit.collider is null)
                return;
            
            DraggedObject = hit.collider.gameObject;
            
            CurrentCoroutine = DragProcess();
            StartCoroutine(CurrentCoroutine);
        }
        
        private void StopDetect(InputAction.CallbackContext context)
        {
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }

        private IEnumerator DragProcess()
        {
            var plane = new Plane(Vector3.forward, Vector3.zero);
            
            while (true)
            {
                var ray = MainCamera.ScreenPointToRay(MousePos);

                if (plane.Raycast(ray, out var enter))
                {
                    var hitPoint = ray.GetPoint(enter);
                    DraggedObject.transform.position = hitPoint;
                }

                yield return null;
            }
        }
    }
}