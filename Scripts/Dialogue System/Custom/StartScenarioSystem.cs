using UI_System.Title_UI_System.Main;
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
            TitleUISystem.StartScenario += StartScenario;
            
            // BattleSystem.StartScenario += StartScenario;
            // ActiveActions.Enqueue(() => BattleSystem.StartScenario -= StartScenario);
        }

        private void OnDestroy()
        {
            TitleUISystem.StartScenario -= StartScenario;
        }

        private void StartScenario(string label, int page)
        {
            AdvEngine.StartScenario(label, page);
        }
    }
}