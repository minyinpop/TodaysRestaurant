using Title_System;
using UnityEngine;
using Utage;

namespace Dialogue_System.Custom
{
    internal sealed class StartScenarioSystem : MonoBehaviour
    {
        [field: Header("Utage")]
        [field: SerializeField] private AdvEngine AdvEngine;
        
        private void Awake()
        {
            // TitleSystem.StartScenario += StartScenario;
            
            // BattleSystem.StartScenario += StartScenario;
            // ActiveActions.Enqueue(() => BattleSystem.StartScenario -= StartScenario);
        }

        private void OnDestroy()
        {
            // TitleSystem.StartScenario -= StartScenario;
        }

        private void StartScenario(string label, int page)
        {
            AdvEngine.StartScenario(label, page);
        }
    }
}