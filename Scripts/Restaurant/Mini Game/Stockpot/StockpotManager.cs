using System.Collections;
using Restaurant.Kitchenware;
using UnityEngine;

namespace Restaurant.Mini_Game.Stockpot
{
    [RequireComponent(typeof(SpoonManager))]
    [RequireComponent(typeof(ProgressBarManager))]
    public class StockpotManager : MiniGameBase
    {
        private KitchenwareGame KitchenwareGame { get; set; }
        private ProgressBarManager ProgressBarManager { get; set; }
        
        private bool IsFinish { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }

        private void Awake()
        {
            ProgressBarManager = GetComponent<ProgressBarManager>();
        }
        
        private void OnDestroy()
        {
            ProgressBarManager.FinishStirring -= Finish;
            
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }
        }
        
        public override void Init(KitchenwareGame game)
        {
            KitchenwareGame = game;
            
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
            
            ProgressBarManager.FinishStirring += Finish;
        }
        
        private IEnumerator MainProcess()
        {
            yield return new WaitUntil(() => IsFinish);
            yield return new WaitForSeconds(1);

            KitchenwareGame.OnGameFinish();
            Destroy(gameObject);
        }

        private void Finish() => IsFinish = true;
    }
}