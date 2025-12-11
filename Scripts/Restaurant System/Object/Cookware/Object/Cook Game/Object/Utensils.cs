using System;
using System.Collections;
using Input_System.Main;
using UnityEngine;

namespace Restaurant_System.Object.Cookware.Object.Cook_Game.Object
{
    internal sealed class Utensils : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Rigidbody2D Rig2D;

        [Header("Physics Settings")]
        [SerializeField] private float MaxSpeed   = 10;
        [SerializeField] private float Accel      = 80;
        [SerializeField] private float SlowRadius = 1;
        [SerializeField] private float StopRadius = .02f;
        
        private float OriginalGravity;

        private bool CanPick = true;
        private bool IsPicked;

        private Plane DragPlane;
        private Vector3 DragOffset;
        private Camera MainCamera;

        private IEnumerator MoveCor;

        public event Action<Vector2> OnPick;

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
            if (!CanPick) return;
            IsPicked = !IsPicked;
            if (IsPicked) Pick();
            else UnPick();
            return;

            void Pick()
            {
                MainCamera = mainCamera;

                InputSystem.GetMousePosition(out var screenPos);
                
                var ray = MainCamera.ScreenPointToRay(screenPos);
                var isNormal = - MainCamera.transform.forward;
                var isPoint = Physics2D.GetRayIntersection(ray);

                DragPlane = new Plane(isNormal, isPoint.point);
                DragPlane.Raycast(ray, out var enter0);

                var worldOnPlane = ray.GetPoint(enter0);
                DragOffset = transform.position - worldOnPlane;

                OriginalGravity = Rig2D.gravityScale;
                Rig2D.gravityScale = 0;

                MoveCor = MoveCoroutine();
                StartCoroutine(MoveCor);
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

                        var p = Rig2D.position;
                        var t = new Vector2(target.x, target.y);
                        var toTarget = t - p;
                        var dist = toTarget.magnitude;
                        var dir = dist > 1e-4f ? toTarget / dist : Vector2.zero;
                        var targetSpeed = dist > SlowRadius ? MaxSpeed : MaxSpeed * (dist / SlowRadius);
                        var desiredVel = dir * targetSpeed;

                        Rig2D.linearVelocity = Vector2.MoveTowards(
                            current: Rig2D.linearVelocity,
                            target: desiredVel,
                            maxDistanceDelta: Accel * Time.fixedDeltaTime);
                        
                        OnPick?.Invoke(desiredVel);
                        
                        if (dist < StopRadius && Mathf.Approximately(Rig2D.linearVelocity.sqrMagnitude, .0025f))
                        {
                            Rig2D.position = t;
                            Rig2D.linearVelocity = Vector2.zero;
                        }
                    }

                    yield return new WaitForFixedUpdate();
                }

                MoveCor = null;
            }
        }

        public void Reset()
        {
            Rig2D.linearVelocity = Vector2.zero;
            Rig2D.rotation = 0;
        }

        public void Disable()
        {
            CanPick = false;
            UnPick();
        }

        private void UnPick()
        {
            if (MoveCor is not null)
            {
                StopCoroutine(MoveCor);
                MoveCor = null;
            }

            Rig2D.gravityScale = OriginalGravity;

            if (Mathf.Approximately(Rig2D.linearVelocity.sqrMagnitude, .0025f))
                Rig2D.linearVelocity = Vector2.zero;
        }
    }
}