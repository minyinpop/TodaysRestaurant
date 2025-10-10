using UnityEngine;
using UnityEngine.UI;

namespace General.Object
{
    internal sealed class ProgressBar : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private Slider Fill;
        
        [field: Header("Value")]
        [field: SerializeField] private float MaxValue;
        [field: SerializeField] private float MinValue;
        [field: SerializeField] private float CurrentValue;

        public void Add()
        {
        }
    }
}