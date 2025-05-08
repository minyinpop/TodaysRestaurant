using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Mini_Game.Stockpot
{
    public class ProgressBarManager : MonoBehaviour
    {
        [field: Header("進度條的遊戲物件")]
        [field: SerializeField] private Slider ProgressBar { get; set; }
        
        private float TargetValue { get; set; }

        private void Update()
        {
            ProgressBar.value = TargetValue;
        }
        
        private void OnEnable() => SpoonManager.AddProgressBarValue += AddValue;
        
        private void OnDisable() => SpoonManager.AddProgressBarValue -= AddValue;

        private void AddValue()
        {
            TargetValue += 0.01f;
        }
    }
}