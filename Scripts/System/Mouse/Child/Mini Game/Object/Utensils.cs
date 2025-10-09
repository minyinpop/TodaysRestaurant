using System.Collections;
using System.Input.Main;
using UnityEngine;

namespace System.Mouse.Child.Mini_Game.Object
{
    internal sealed class Utensils : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Rigidbody2D Rig2D;

        private bool IsPicked;
        
        private Plane DragPlane;
        private Vector3 DragOffset;
        private Camera MainCamera;

        private IEnumerator MoveCor;

        private void OnDisable()
        {
            if (MoveCor is not null)
            {
                StopCoroutine(MoveCor);
                MoveCor = null;
            }
        }

        public void OnClick(Camera mainCamera)
        {
            IsPicked = !IsPicked;
            if (IsPicked) Pick();
            else UnPick();
            return;

            void Pick()
            {
                MainCamera = mainCamera;

                Rig2D.freezeRotation = true;

                InputSystem.GetMousePosition(out var screenPos);
                var ray = MainCamera.ScreenPointToRay(screenPos);
                var isNormal = - MainCamera.transform.forward;
                var isPoint = Physics2D.GetRayIntersection(ray);
                
                DragPlane = new Plane(isNormal, isPoint.point);
                DragPlane.Raycast(ray, out var enter0);
                
                var worldOnPlane = ray.GetPoint(enter0);
                DragOffset = transform.position - worldOnPlane;
                
                MoveCor = MoveCoroutine();
                StartCoroutine(MoveCor);
            }

            void UnPick()
            {
                Rig2D.freezeRotation = false;
                
                StopCoroutine(MoveCor);
                MoveCor = null;
            }

            IEnumerator MoveCoroutine()
            {
                while (IsPicked)
                {
                    InputSystem.GetMousePosition(out var screenPos);
                    var ray = MainCamera.ScreenPointToRay(screenPos);
                    
                    if (DragPlane.Raycast(ray, out var enter))
                    {
                        var worldOnPlane = ray.GetPoint(enter);
                        var target = worldOnPlane + DragOffset;

                        target.z = transform.position.z;

                        if (Rig2D is not null) Rig2D.MovePosition(new Vector2(target.x, target.y));
                        else transform.position = target;
                    }

                    yield return null;
                }

                MoveCor = null;
            }
        }

        public void Reset()
        {
            Rig2D.linearVelocity = Vector2.zero;
            Rig2D.rotation = 0;
            Rig2D.freezeRotation = false;
        }
    }
}