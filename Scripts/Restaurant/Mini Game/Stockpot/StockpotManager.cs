using UnityEngine;

namespace Restaurant.Mini_Game.Stockpot
{
    public class StockpotManager : MonoBehaviour
    {
        [field: Header("自身的遊戲物件")]
        [field: SerializeField] private GameObject Spoon { get; set; }
        [field: SerializeField] private GameObject ProgressBar { get; set; }
        private SpoonManager SpoonManager { get; set; }
        private ProgressBarManager ProgressBarManager { get; set; }

        private void Awake()
        {
            SpoonManager = Spoon.GetComponent<SpoonManager>();
            ProgressBarManager = ProgressBar.GetComponent<ProgressBarManager>();
        }
    }
}