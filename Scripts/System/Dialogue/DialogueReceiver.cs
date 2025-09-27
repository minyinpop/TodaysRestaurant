using System.Title.Main;
using UnityEngine;
using Utage;

namespace System.Dialogue
{
    internal sealed class DialogueReceiver : MonoBehaviour
    {
        [field: SerializeField] private AdvEngine AdvEngine;

        private void OnEnable()
        {
            TitleSystem.PlayDialogue += StartScenario;
        }

        private void OnDisable()
        {
            TitleSystem.PlayDialogue -= StartScenario;
        }

        private void StartScenario(string label, int page)
        {
            AdvEngine.StartScenario(label, page);
        }
    }
}