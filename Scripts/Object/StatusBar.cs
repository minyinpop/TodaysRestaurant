using UnityEngine;
using UnityEngine.UI;

namespace Object
{
    internal sealed class StatusBar : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private Image InnerFill;
        [field: SerializeField] private Image OuterFill;

        private float MaxValue = 1;
        private float MinValue = 0;
        
        private float CurrentValue;

        public void Init(float minValue, float maxValue)
        {
            MaxValue = maxValue;
            MinValue = minValue;
        }
    }
}