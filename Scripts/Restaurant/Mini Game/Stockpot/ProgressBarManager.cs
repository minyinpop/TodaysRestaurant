using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Mini_Game.Stockpot
{
    internal class ProgressBarManager : MonoBehaviour
    {
        [field: Header("進度條的遊戲物件")]
        [field: SerializeField] private Slider ProgressBar { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        private float TargetValue { get; set; }
        public event Action FinishStirring;

        private void OnEnable()
        {
            SpoonManager.AddProgressBarValue += AddValue;
            
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }
        
        private void OnDisable()
        {
            SpoonManager.AddProgressBarValue -= AddValue;
            
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }
        }

        private void AddValue()
        {
            // TargetValue += ProgressBar.maxValue / 50;
            TargetValue += 100; // For Dev Only

        }

        private IEnumerator MainProcess()
        {
            while (ProgressBar.value < ProgressBar.maxValue)
            {
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, TargetValue, Mathf.Abs(TargetValue - ProgressBar.value) * 1 * Time.deltaTime);
                yield return null;
            }

            FinishStirring?.Invoke();
        }
    }
}