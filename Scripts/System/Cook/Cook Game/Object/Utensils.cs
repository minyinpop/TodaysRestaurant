using System.Collections;
using System.Input.Main;
using UnityEngine;

namespace System.Cook.Cook_Game.Object
{
    internal sealed class Utensils : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private Rigidbody2D Rig2D;

        [Header("Physics Settings")]
        [SerializeField] private float MaxSpeed   = 6f;
        [SerializeField] private float Accel      = 40f;
        [SerializeField] private float SlowRadius = 1.0f;
        [SerializeField] private float StopRadius = 0.02f;
        
        private float OriginalGravity;
        
        private bool IsPicked;
        private bool InDetectArea;

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

            void UnPick()
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

        public void IsInDetectArea(bool inDetectArea)
        {
            InDetectArea = inDetectArea;
        }
    }
}