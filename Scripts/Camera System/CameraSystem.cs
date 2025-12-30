using Unity.Cinemachine;
using UnityEngine;

namespace Camera_System
{
    public sealed class CameraSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private CinemachineCamera cinemachineCamera;
        private static CinemachineCamera _cinemachineCamera;
        private static CinemachineFollow _cinemachineFollow;
        private static CinemachineHardLookAt _cinemachineHardLookAt;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private Transform originParent;
        private static Transform _originParent;
        [field: SerializeField] private Vector3 originFollowOffset;
        private static Vector3 _originFollowOffset;
        [field: SerializeField] private Vector3 originLookAtOffset;
        private static Vector3 _originLookAtOffset;
        
        private void Awake()
        {
            _cinemachineCamera = cinemachineCamera;
            _cinemachineFollow = cinemachineCamera.GetComponent<CinemachineFollow>();
            _cinemachineHardLookAt = cinemachineCamera.GetComponent<CinemachineHardLookAt>();
            
            _originParent = originParent;
            _originFollowOffset = originFollowOffset;
            _originLookAtOffset = originLookAtOffset;
        }
        
        public static void MoveTo(Transform target, Vector3 followOffset, Vector3 lookAtOffset)
        {
            _cinemachineCamera.Target.TrackingTarget = target;
            _cinemachineFollow.FollowOffset = followOffset;
            _cinemachineHardLookAt.LookAtOffset = lookAtOffset;
        }

        public static void MoveBack()
        {
            _cinemachineCamera.Target.TrackingTarget = _originParent;
            _cinemachineFollow.FollowOffset = _originFollowOffset;
            _cinemachineHardLookAt.LookAtOffset = _originLookAtOffset;
        }
    }
}