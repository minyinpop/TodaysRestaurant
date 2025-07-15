using UnityEngine;

namespace UTILITY
{
    [System.Serializable]
    internal class AnchorsMult
    {
        [field: Header("X 軸")]
        [field: SerializeField, Range(0, 1)] public float XMax;
        [field: SerializeField, Range(0, 1)] public float XMin;
        
        [field: Header("Y 軸")]
        [field: SerializeField, Range(0, 1)] public float YMax;
        [field: SerializeField, Range(0, 1)] public float YMin;

        public float GetRandomXMult()
        {
            return Random.Range(XMin, XMax);
        }
        
        public float GetRandomYMult()
        {
            return Random.Range(YMin, YMax);
        }
    }
}