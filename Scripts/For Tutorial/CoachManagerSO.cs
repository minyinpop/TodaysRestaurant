using UnityEngine;

namespace For_Tutorial
{
    [CreateAssetMenu(menuName = "Minyinpop/CoachManagerSO", fileName = "New Data", order = 1)]
    public class CoachManagerSO : ScriptableObject
    {
        [Header("縮放")]
        public Vector2 targetPos;
        public Vector2 initialSize;
        public Vector2 targetSize;
        public float zoomSpeed;
    }
}