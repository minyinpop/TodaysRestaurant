using System.Collections;
using UnityEngine;

namespace Restaurant.Mini_Game.Stockpot
{
    [RequireComponent(typeof(SpoonManager))]
    [RequireComponent(typeof(ProgressBarManager))]
    public class StockpotManager : MonoBehaviour
    {
        private bool IsFinish { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }

        private void Start()
        {
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }
        
        private void OnEnable() => ProgressBarManager.FinishStirring += Finish;
        
        private void OnDisable()
        {
            ProgressBarManager.FinishStirring -= Finish;
            
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }
        }
        
        private IEnumerator MainProcess()
        {
            yield return new WaitUntil(() => IsFinish);
            yield return new WaitForSeconds(1);
            
            Destroy(gameObject);
        }

        private void Finish() => IsFinish = true;
    }
}