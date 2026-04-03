using UnityEngine;
using Utage;

namespace Dialogue_System.Custom
{
    internal sealed class DialogueSystem : MonoBehaviour
    {
        [field: Header("Utage")]
        [field: SerializeField] private AdvEngine AdvEngine;

        public void StartSystem(string label, int page)
        {
            AdvEngine.StartScenario(label, page);
        }
    }
}