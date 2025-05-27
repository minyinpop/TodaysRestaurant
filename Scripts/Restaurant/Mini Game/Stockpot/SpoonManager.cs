using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Restaurant.Mini_Game.Stockpot
{
    internal class SpoonManager : MonoBehaviour
    {
        [field: Header("湯勺的圖層名稱")]
        [field: SerializeField] private LayerMask SpoonLayer { get; set; }
        private GameObject Spoon { get; set; }
        private Rigidbody2D SpoonRig { get; set; }
        
        private InputManager Input { get; set; }
        private Vector2 MousePos => Input.Mouse.MousePos.ReadValue<Vector2>();
        
        private Camera MainCamera { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }
        private bool IsStirring { get; set; }
        private Vector2 LastMousePos { get; set; }
        
        public static event Action AddProgressBarValue;
        public static event Action OnClickEvent;

        private void Awake()
        {
            Input = InputSystem.Input;
            MainCamera = Camera.main;
        }

        private void OnEnable()
        {
            Input.Mouse.LeftClick.started += StartDetect;
            Input.Mouse.LeftClick.canceled += StopDetect;

            StirringAreaManager.DraggableObjectEnter += OnEnterStirringArea;
            StirringAreaManager.DraggableObjectExit += OnLeaveStirringArea;
        }

        private void OnDisable()
        {
            Input.Mouse.LeftClick.started -= StartDetect;
            Input.Mouse.LeftClick.canceled -= StopDetect;
            
            StirringAreaManager.DraggableObjectEnter -= OnEnterStirringArea;
            StirringAreaManager.DraggableObjectExit -= OnLeaveStirringArea;
            
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }

        private void StartDetect(InputAction.CallbackContext context)
        {
            var plane = new Plane(Vector3.forward, transform.position);
            var ray = MainCamera.ScreenPointToRay(MousePos);

            if (!plane.Raycast(ray, out var enter))
                return;
            
            var hitPoint = ray.GetPoint(enter);
            var hit = Physics2D.Raycast(hitPoint, Vector2.zero, Mathf.Infinity, SpoonLayer.value);

            if (hit.collider is null)
                return;
            OnClickEvent?.Invoke();
            Spoon = hit.collider.gameObject;
            SpoonRig = Spoon.GetComponent<Rigidbody2D>();
            
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
            var plane = new Plane(Vector3.forward, transform.position);
            
            while (true)
            {
                var ray = MainCamera.ScreenPointToRay(MousePos);

                plane.Raycast(ray, out var enter);
                
                var hitPoint = ray.GetPoint(enter);
                
                Spoon.transform.position = hitPoint;
                SpoonRig.linearVelocity = Vector2.zero;

                if (IsStirring)
                {
                    var currentPos = hitPoint;
                    var distance = Vector2.Distance(LastMousePos, currentPos);

                    if (distance > .1f)
                    {
                        AddProgressBarValue?.Invoke();
                        LastMousePos = currentPos;
                    }
                }
                
                yield return null;
            }
        }

        private void OnEnterStirringArea() => IsStirring = true;
        
        private void OnLeaveStirringArea() => IsStirring = false;
    }
}