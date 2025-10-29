using System.Battle.System.Main;
using System.Collections.Generic;
using System.Title;
using UnityEngine;
using Utage;

namespace System.Dialogue.Custom
{
    internal sealed class StartScenarioSystem : MonoBehaviour
    {
        [field: Header("Utage")]
        [field: SerializeField] private AdvEngine AdvEngine;
        
        private readonly Queue<Action> ActiveActions = new();

        private void OnEnable()
        {
            TitleSystem.StartScenario += StartScenario;
            ActiveActions.Enqueue(() => TitleSystem.StartScenario -= StartScenario);
            
            BattleSystem.StartScenario += StartScenario;
            ActiveActions.Enqueue(() => BattleSystem.StartScenario -= StartScenario);
        }

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }

        private void StartScenario(string label, int page)
        {
            AdvEngine.StartScenario(label, page);
        }
    }
}